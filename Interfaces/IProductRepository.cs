

using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces
{
    public interface IProductRepository
    {
        IList<ProductModel> GetProducts();
        ProductModel GetProductById(int id);
        IList<ProductModel> GetProductByCategory(int categoryId);
        bool CreateProduct(ProductModel product, List<int> categoryId, int brandId);
    }
}