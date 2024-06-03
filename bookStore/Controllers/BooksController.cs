using bookStore.Data;
using bookStore.Models;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = context.authors.OrderBy(x => x.Name).ToList();
            var authorList = new List<SelectListItem>();
            foreach (var author in authors)
            {
                authorList.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = author.Name,
                });
            }
            var categories = context.categories.OrderBy(x => x.Name).ToList();
            var categoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }
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


            var book = new Book
            {
                Title = viewModel.Title,
                AuthorId = viewModel.AuthorId,
                Publisher = viewModel.Publisher,
                PublisherDate = viewModel.PublisherDate,
                Description = viewModel.Description,
                Categories = viewModel.SelectedCategories.Select(Id => new BookCategory { CategoryId = Id }).ToList()
            };

            Console.WriteLine($"{book.Categories.First().Category}");

            context.books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

