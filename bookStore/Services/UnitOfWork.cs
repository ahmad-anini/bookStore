using bookStore.Data;
using bookStore.Repositories;
using bookStore.Repositories.Implementaion;

namespace bookStore.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _wepHost;
        private readonly ILogger _logger;

        public ICategoryRepository CategoryRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext, IWebHostEnvironment WepHost, ILogger logger)
        {
            _dbContext = dbContext;
            _wepHost = WepHost;
            _logger = logger;
            CategoryRepository = new CategoryRepository(_dbContext);
            AuthorRepository = new AuthorRepository(_dbContext);
            BookRepository = new BookRepository(_dbContext, WepHost);
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
