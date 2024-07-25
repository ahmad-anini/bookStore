using AutoMapper;
using bookStore.Models;
using bookStore.Services.UnitOfWorkService;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class AuthorsController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var authors = unitOfWork.AuthorRepository.GetAll();
            var authorsVM = mapper.Map<List<AuthorVM>>(authors);
            return View(authorsVM);
        }

        public IActionResult Detales(int id)
        {
            var author = unitOfWork.AuthorRepository.GetById(id);
            if (author is null)
            {
                return NotFound();
            }
            var viewModle = mapper.Map<AuthorVM>(author);

            return View(viewModle);

        }

        [HttpGet]
        public IActionResult Create() => View("Form");

        [HttpPost]
        public IActionResult Create(AuthorFormVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", authorVM);
            }
            var author = mapper.Map<Author>(authorVM);
            unitOfWork.AuthorRepository.CreateAuthor(author);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = unitOfWork.AuthorRepository.GetById(id);
            if (author is null)
            {
                return NotFound();
            }

            var authorVM = mapper.Map<AuthorFormVM>(author);
            return View("Form", authorVM);
        }

        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorVM)
        {
            var author = unitOfWork.AuthorRepository.GetById(authorVM.Id);
            if (author is null)
            {
                return NotFound();
            }

            author.Name = authorVM.Name;
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var author = unitOfWork.AuthorRepository.GetById(id);
            if (author is null)
            {
                return NotFound();
            }

            unitOfWork.AuthorRepository.DeleteAuthor(author);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
