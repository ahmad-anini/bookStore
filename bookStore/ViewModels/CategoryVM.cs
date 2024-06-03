using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModels
{
    public class CategoryVM
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = default!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
