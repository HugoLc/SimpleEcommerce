using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces
{
    public interface ISkuRepository
    {
        IList<SkuModel> GetSkus();
        SkuModel GetSku(int id);
        bool CreateSku(SkuModel sku, int productId);
    }
}