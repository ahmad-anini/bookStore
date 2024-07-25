using AutoMapper;
using bookStore.Models;
using bookStore.Services;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var categories = unitOfWork.CategoryRepository.GetAll();

            var categoryVM = mapper.Map<List<CategoryVM>>(categories);

            return View(categoryVM);
        }

        public IActionResult Details(int id)
        {
            var category = unitOfWork.CategoryRepository.GetById(id);
            if (category is null)
            {
                return NotFound();
            }
            var viewModle = mapper.Map<CategoryVM>(category);

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
            var category = mapper.Map<Category>(categoryVM);
            try
            {
                unitOfWork.CategoryRepository.CreateCategory(category);
                unitOfWork.Save();
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
            var category = unitOfWork.CategoryRepository.GetById(categoryVM.Id);
            if (category is null)
            {
                return NotFound();
            }

            category.Name = categoryVM.Name;
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = unitOfWork.CategoryRepository.GetById(id);
            if (category is null)
            {
                return NotFound();
            }

            unitOfWork.CategoryRepository.DeleteCategory(category);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
