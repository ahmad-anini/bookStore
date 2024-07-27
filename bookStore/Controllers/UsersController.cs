using AutoMapper;
using bookStore.Models;
using bookStore.Services.UnitOfWorkService;
using bookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var viewModelList = _mapper.Map<List<ApplicationUserVM>>(users);

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var viewModel = viewModelList.FirstOrDefault(vm => vm.UserName == user.UserName);

                if (viewModel != null)
                {
                    viewModel.Roles = roles.ToList();
                }
            }
            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new ApplicationUserCreateVM()
            {
                Roles = _mapper.Map<List<SelectListItem>>(roles)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = _mapper.Map<ApplicationUser>(viewModel);

            var result = await _userManager.CreateAsync(user, viewModel.PasswordHash);
            if (!result.Succeeded)
            {
                return View(viewModel);
            }
            await _userManager.AddToRolesAsync(user, viewModel.SelectedRoles);

            return RedirectToAction("Index");
        }

    }
}
