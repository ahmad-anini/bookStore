using bookStore.Models;

namespace bookStore.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book? GetById(int id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        string AddBookImage(IFormFile img);
        void DeleteBookImage(string imgUrl);
    }
}
