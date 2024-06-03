using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModels
{
    public class AuthorFormVM
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;
    }
}
