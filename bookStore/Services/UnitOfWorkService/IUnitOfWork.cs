using bookStore.Repositories;

namespace bookStore.Services.UnitOfWorkService
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        int Save();
    }
}
