using bookStore.Data;
using bookStore.Models;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.categories.ToList();

            var categoryVM = categories.Select(category => new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
            }).ToList();
            return View(categoryVM);
        }

        public IActionResult Detales(int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var viewModle = new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                UpdatedOn = category.UpdatedOn,

            };

            return View(viewModle);

        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryVM);
            }
            var category = new Category { Name = categoryVM.Name };
            try
            {
                context.categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Category Name Already Exists");
                return View(categoryVM);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id) => View("Create");

        [HttpPost]
        public IActionResult Edit(CategoryVM categoryVM)
        {
            var category = context.categories.Find(categoryVM.Id);
            if (category is null)
            {
                return NotFound();
            }

            category.Name = categoryVM.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }

            context.categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
