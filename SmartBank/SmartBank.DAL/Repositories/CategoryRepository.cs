using SmartBank.DAL.Data;
using SmartBank.DAL.Interfaces;
using SmartBank.DAL.Models;

namespace SmartBank.DAL.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly SmartBankDBContext _smartBankDBContext;

        public CategoryRepository(SmartBankDBContext smartBankDBContext)
        {
            _smartBankDBContext = smartBankDBContext;
        }

        public List<Category> GetDefaultCategories()
        {
            return _smartBankDBContext.Category.Where(c => c.IsDefault).ToList();
        }

        public List<Category> GetUserCategories(int userId)
        {
            return _smartBankDBContext.Category.Where(c => c.UserId == userId).ToList();
        }

        public void AddCategory(Category category)
        {
            _smartBankDBContext.Category.Add(category);
            _smartBankDBContext.SaveChanges();
        }
    }
}
