using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces;

public interface ICategoryRepository
{
    IList<CategoryModel> GetCategories();
    CategoryModel GetCategory(int id);
    bool CreateCategory(CategoryModel category);
}