

using SimpleEcommerce.Dto.Request;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces
{
    public interface IProductRepository
    {
        IList<ProductModel> GetProducts();
        ProductModel GetProductById(int id);
        bool DeleteProduct(int id);
        IList<ProductModel> GetProductsByCategory(int categoryId);
        IList<ProductModel> GetProductsByBrand(int brandId);
        ProductModel GetProductBySlug(string slug);
        ProductModel CreateProduct(ProductModel product, List<int> categoryId, int brandId, List<SkuModel> skus);
        ProductModel UpdateProduct(ProductUpdateReqDto product, int productId);
    }
}