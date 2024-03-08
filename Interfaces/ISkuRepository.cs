using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces
{
    public interface ISkuRepository
    {
        IList<SkuModel> GetSkus();
        SkuModel GetSku(int id);
        bool DeleteSku(int id);
        SkuModel UpdateSku(SkuModel sku);
        bool CreateSku(SkuModel sku, int productId);
    }
}