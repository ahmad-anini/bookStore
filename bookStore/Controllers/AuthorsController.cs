using bookStore.Data;
using bookStore.Models;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context) { 
            this.context = context;
        }

        public IActionResult Index()
        {
            var authors = context.authors.ToList();
            var authorsVM = new List<AuthorVM>();
            foreach (var author in authors)
            {
                var authorVM = new AuthorVM() { 
                Id = author.Id,
                Name = author.Name,
                CreatedOn = author.CreatedOn,
                UpdatedOn = author.UpdatedOn,  
                };
                authorsVM.Add(authorVM);
            }
            return View(authorsVM);
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
            var author = new Author { Id = authorVM.Id ,Name = authorVM.Name };
            context.authors.Add(author);
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id) {
          var author =  context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }

            var authorVM = new AuthorFormVM
            {
                Id = author.Id,
                Name = author.Name,
            };
        return View("Form", authorVM);
        } 

        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorVM)
        {
            var author = context.authors.Find(authorVM.Id);
            if (author is null)
            {
                return NotFound();
            }

            author.Name = authorVM.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var author = context.authors.Find(id);
            if (author is null)
            {
                return NotFound();
            }

            context.authors.Remove(author);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
