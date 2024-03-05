using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces;

public interface IBrandRepository
{
    IList<BrandModel> GetBrands();
    BrandModel GetBrand(int id);
    bool CreateBrand(BrandModel brand);
    BrandModel UpdateBrand(BrandModel brand);
}