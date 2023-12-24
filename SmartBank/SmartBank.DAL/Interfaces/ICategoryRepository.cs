using SmartBank.DAL.Models;

namespace SmartBank.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetDefaultCategories();
        List<Category> GetUserCategories(int userId);
        void AddCategory(Category category);
        Category? GetCategoryById(int id);
        Category GetUndefinedCategory();
    }
}
