using AutoMapper;
using bookStore.Models;
using bookStore.Services.UnitOfWorkService;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BooksController(IUnitOfWork unitOfWork,
                IMapper mapper,
                ILogger<BooksController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var books = _unitOfWork.BookRepository.GetAll();

            var bookvm = _mapper.Map<List<BookVM>>(books);

            return View(bookvm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = _unitOfWork.AuthorRepository.GetAll();

            var authorList = _mapper.Map<List<SelectListItem>>(authors);

            var categories = _unitOfWork.CategoryRepository.GetAll();

            var categoryList = _mapper.Map<List<SelectListItem>>(categories);

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
                _unitOfWork.BookRepository.AddBookImage(viewModel.ImageUrl) : string.Empty;

                var book = _mapper.Map<Book>(viewModel);
                book.ImageUrl = imageName;

                _unitOfWork.BookRepository.CreateBook(book);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        public ActionResult Delete(int id)
        {
            var book = _unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            try
            {
                if (book.ImageUrl != null)
                {
                    _unitOfWork.BookRepository.DeleteBookImage(book.ImageUrl);
                }

                _unitOfWork.BookRepository.DeleteBook(book);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }


            return Ok();
        }
    }
}

