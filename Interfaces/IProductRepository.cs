

using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces
{
    public interface IProductRepository
    {
        IList<ProductModel> GetProducts();
        ProductModel GetProduct(int id);
        bool CreateProduct(ProductModel product, List<int> categoryId);
    }
}