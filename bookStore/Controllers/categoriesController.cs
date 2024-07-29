using AutoMapper;
using bookStore.Models;
using bookStore.Services.UnitOfWorkService;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();

            var categoryVM = _mapper.Map<List<CategoryVM>>(categories);

            return View(categoryVM);
        }

        public IActionResult Details(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category is null)
            {
                return NotFound();
            }
            var viewModle = _mapper.Map<CategoryVM>(category);

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
            var category = _mapper.Map<Category>(categoryVM);
            try
            {
                _unitOfWork.CategoryRepository.CreateCategory(category);
                _unitOfWork.Save();
                return Ok();
            }
            catch
            {
                ModelState.AddModelError("Name", "Category Name Already Exists");
                return View(categoryVM);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id) => View("Edit");

        [HttpPost]
        public IActionResult Edit(CategoryVM categoryVM)
        {
            var category = _unitOfWork.CategoryRepository.GetById(categoryVM.Id);
            if (category is null)
            {
                return NotFound();
            }

            category.Name = categoryVM.Name;
            if (!ModelState.IsValid)
            {
                return View("Edit", categoryVM);
            }
            _unitOfWork.Save();
            return Ok();
        }

        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category is null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepository.DeleteCategory(category);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
