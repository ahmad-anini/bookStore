using bookStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class categoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public categoriesController(ApplicationDbContext context) {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.categories.ToList();
            return View(categories);
        }
    }
}
