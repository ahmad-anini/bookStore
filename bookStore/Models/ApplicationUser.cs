using Microsoft.AspNetCore.Identity;

namespace bookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;

    }
}
