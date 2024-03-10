using SimpleEcommerce.Models;

namespace SimpleEcommerce.Interfaces;

public interface IBrandRepository
{
    IList<BrandModel> GetBrands();
    BrandModel GetBrand(int id);
    BrandModel CreateBrand(BrandModel brand);
    bool DeleteBrand(int id);
    BrandModel UpdateBrand(BrandModel brand);
}