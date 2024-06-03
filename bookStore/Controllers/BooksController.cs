using bookStore.Data;
using bookStore.Models;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment WepHost;
        public BooksController(ApplicationDbContext context, IWebHostEnvironment WepHost)
        {
            this.context = context;
            this.WepHost = WepHost;

        }

        public IActionResult Index()
        {
            var books = context.books
                .Include(book => book.Author)
                .Include(book => book.Categories)
                .ThenInclude(book => book.Category)
                .ToList();


            var bookvm = books.Select(book => new BookVM
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                PublisherDate = book.PublisherDate,
                Auther = book.Author.Name,
                imageURL = book.ImageUrl,
                Categories = book.Categories.Select(book => book.Category.Name).ToList()
            }).ToList();
            return View(bookvm);
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

            String imageName = "";
            if (viewModel.ImageUrl != null)
            {

                imageName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_" + Path.GetFileName(viewModel.ImageUrl.FileName);
                var path = Path.Combine($"{WepHost.WebRootPath}/image/Books", imageName);
                var stream = System.IO.File.Create(path);
                viewModel.ImageUrl.CopyTo(stream);
            }

            var book = new Book
            {
                Title = viewModel.Title,
                AuthorId = viewModel.AuthorId,
                Publisher = viewModel.Publisher,
                PublisherDate = viewModel.PublisherDate,
                Description = viewModel.Description,
                ImageUrl = imageName,
                Categories = viewModel.SelectedCategories.Select(Id => new BookCategory { CategoryId = Id }).ToList()
            };

            Console.WriteLine($"{book.Categories.First().Category}");

            context.books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var book = context.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            if (book.ImageUrl != null)
            {
                var path = Path.Combine($"{WepHost.WebRootPath}/image/Books", book.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            context.books.Remove(book);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

