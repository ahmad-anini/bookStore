using AutoMapper;
using bookStore.Models;
using bookStore.Services.UnitOfWorkService;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public BooksController(IUnitOfWork unitOfWork,
                IMapper mapper,
                ILogger<BooksController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var books = unitOfWork.BookRepository.GetAll();

            var bookvm = mapper.Map<List<BookVM>>(books);

            return View(bookvm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = unitOfWork.AuthorRepository.GetAll();

            var authorList = mapper.Map<List<SelectListItem>>(authors);

            var categories = unitOfWork.CategoryRepository.GetAll();

            var categoryList = mapper.Map<List<SelectListItem>>(categories);

            var viewModel = new BookFormVM
            {
                Authors = authorList,
                Categories = categoryList
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(BookFormVM viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var imageName = viewModel.ImageUrl != null ?
                unitOfWork.BookRepository.AddBookImage(viewModel.ImageUrl) : string.Empty;

                var book = mapper.Map<Book>(viewModel);
                book.ImageUrl = imageName;

                unitOfWork.BookRepository.CreateBook(book);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }



            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var book = unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            try
            {
                if (book.ImageUrl != null)
                {
                    unitOfWork.BookRepository.DeleteBookImage(book.ImageUrl);
                }

                unitOfWork.BookRepository.DeleteBook(book);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }


            return RedirectToAction("Index");
        }
    }
}

