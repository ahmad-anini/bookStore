namespace bookStore.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string Auther { get; set; } = default!;

        public string Publisher { get; set; } = default!;
        public DateTime PublisherDate { get; set; }
        public string imageURL { get; set; } = default!;
        public List<string> Categories { get; set; } = new List<string>();
    }
}
