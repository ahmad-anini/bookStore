using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModels
{
    public class BookFormVM
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public List<SelectListItem>? Authors { get; set; } = new List<SelectListItem>();
        public string Publisher { get; set; } = default!;
        [Display(Name = "Publisher Date")]
        public DateTime PublisherDate { get; set; } = DateTime.Now;
        [Display(Name = "Image")]
        public IFormFile? ImageUrl { get; set; }
        public List<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
        public List<int> SelectedCategories { get; set; } = new List<int>();
    }
}
