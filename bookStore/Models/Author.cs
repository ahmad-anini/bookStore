namespace bookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public List<Book>? Books { get; set; } = new List<Book>();

    }
}
