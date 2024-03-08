using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces;

public interface ICategoryRepository
{
    IList<CategoryModel> GetCategories();
    CategoryModel GetCategory(int id);
    CategoryModel UpdateCategory(CategoryModel category);
    bool CreateCategory(CategoryModel category);
    bool DeleteCategory(int id);
}