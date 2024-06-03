namespace bookStore.Models
{
    public class BookCategory
    {
        public Book Book { get; set; } = default!;
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
