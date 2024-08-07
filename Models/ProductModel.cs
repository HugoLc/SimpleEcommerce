namespace SimpleEcommerce.Models;

public class ProductModel{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public BrandModel Brand { get; set; }
    public List<CategoryProductModel> CategoryProduct { get; set; }
    public List<SkuModel> Skus { get; set; }
}

