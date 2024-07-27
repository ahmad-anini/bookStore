namespace bookStore.ViewModels
{
    public class ApplicationUserVM
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Address { get; set; } = default!;
        public bool IsDeleted { get; set; } = default!;
        public List<string>? Roles { get; set; } = new List<string>();
    }
}
