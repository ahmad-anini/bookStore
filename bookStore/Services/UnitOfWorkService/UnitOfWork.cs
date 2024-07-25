using bookStore.Data;
using bookStore.Repositories;

namespace bookStore.Services.UnitOfWorkService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ICategoryRepository CategoryRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext,
                ILogger<UnitOfWork> logger,
                ICategoryRepository categoryRepository,
                IAuthorRepository authorRepository,
                IBookRepository bookRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            CategoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            AuthorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            BookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public int Save()
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var result = _dbContext.SaveChanges();
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "An error occurred while saving changes to the database. Transaction rolled back.");
                throw;
            }
        }
    }
}
