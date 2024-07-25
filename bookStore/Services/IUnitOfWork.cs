using bookStore.Repositories;

namespace bookStore.Services
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        int Save();
    }
}
