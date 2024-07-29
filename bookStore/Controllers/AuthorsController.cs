using AutoMapper;
using bookStore.Models;
using bookStore.Services.UnitOfWorkService;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class AuthorsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var authors = _unitOfWork.AuthorRepository.GetAll();
            var authorsVM = _mapper.Map<List<AuthorVM>>(authors);
            return View(authorsVM);
        }

        public IActionResult Detales(int id)
        {
            var author = _unitOfWork.AuthorRepository.GetById(id);
            if (author is null)
            {
                return NotFound();
            }
            var viewModle = _mapper.Map<AuthorVM>(author);

            return View(viewModle);

        }

        [HttpGet]
        public IActionResult Create() => View("Create");

        [HttpPost]
        public IActionResult Create(AuthorFormVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", authorVM);
            }
            var author = _mapper.Map<Author>(authorVM);
            _unitOfWork.AuthorRepository.CreateAuthor(author);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _unitOfWork.AuthorRepository.GetById(id);
            if (author is null)
            {
                return NotFound();
            }

            var authorVM = _mapper.Map<AuthorFormVM>(author);
            return View("Edit", authorVM);
        }

        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorVM)
        {
            var author = _unitOfWork.AuthorRepository.GetById(authorVM.Id);
            if (author is null)
            {
                return NotFound();
            }

            author.Name = authorVM.Name;
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var author = _unitOfWork.AuthorRepository.GetById(id);
            if (author is null)
            {
                return NotFound();
            }

            _unitOfWork.AuthorRepository.DeleteAuthor(author);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
