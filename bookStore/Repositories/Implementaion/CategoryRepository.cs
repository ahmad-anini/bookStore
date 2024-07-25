using bookStore.Data;
using bookStore.Models;

namespace bookStore.Repositories.Implementaion
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateCategory(Category category)
        {
            _dbContext.categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _dbContext.categories.Remove(category);
        }

        public List<Category> GetAll()
        {
            return _dbContext.categories.ToList();
        }

        public Category? GetById(int id)
        {
            return _dbContext.categories.FirstOrDefault(c => c.Id == id);
        }



        public void UpdateCategory(Category category)
        {
            _dbContext.categories.Update(category);
        }
    }
}
