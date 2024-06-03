namespace bookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public DateTime PublisherDate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = default!;
        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();
    }
}
