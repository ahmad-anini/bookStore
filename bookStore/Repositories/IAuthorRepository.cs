using bookStore.Models;

namespace bookStore.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> GetAll();
        Author? GetById(int id);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
