using bookStore.Data;
using bookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Repositories.Implementaion
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _wepHost;

        public BookRepository(ApplicationDbContext dbContext, IWebHostEnvironment WepHost)
        {
            _dbContext = dbContext;
            _wepHost = WepHost;
        }

        public string AddBookImage(IFormFile img)
        {
            string imageName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_" + Path.GetFileName(img.FileName);
            var path = Path.Combine($"{_wepHost.WebRootPath}/image/Books", imageName);
            var stream = System.IO.File.Create(path);
            img.CopyTo(stream);
            return imageName;
        }

        public void CreateBook(Book book)
        {
            _dbContext.books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            _dbContext.books.Remove(book);
        }

        public void DeleteBookImage(string imgUrl)
        {
            var path = Path.Combine($"{_wepHost.WebRootPath}/image/Books", imgUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public List<Book> GetAll()
        {
            return _dbContext.books
                .Include(book => book.Author)
                .Include(book => book.Categories)
                    .ThenInclude(c => c.Category)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _dbContext.books.Find(id);
        }

        public void UpdateBook(Book book)
        {
            _dbContext.books.Update(book);
        }
    }
}
