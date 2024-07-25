using bookStore.Data;
using bookStore.Models;

namespace bookStore.Repositories.Implementaion
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateAuthor(Author author)
        {
            _dbContext.authors.Add(author);
        }

        public void DeleteAuthor(Author author)
        {
            _dbContext.authors.Remove(author);
        }

        public List<Author> GetAll()
        {
            return _dbContext.authors.ToList();
        }

        public Author? GetById(int id)
        {
            return _dbContext.authors.FirstOrDefault(c => c.Id == id);
        }


        public void UpdateAuthor(Author author)
        {
            _dbContext.authors.Update(author);
        }
    }
}
