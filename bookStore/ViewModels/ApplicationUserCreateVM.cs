using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModels
{
    public class ApplicationUserCreateVM
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        [Display(Name = "Password")]
        public string PasswordHash { get; set; } = default!;
        public string Address { get; set; } = default!;
        public bool IsDeleted { get; set; } = default!;
        public List<SelectListItem>? Roles { get; set; } = new List<SelectListItem>();
        public List<string> SelectedRoles { get; set; } = new List<string>();

    }
}
