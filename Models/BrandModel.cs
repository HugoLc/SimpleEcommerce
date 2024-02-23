namespace SimpleEcommerce.Models;

public class BrandModel{
    public int BrandId { get; set; }
    public string Name { get; set; }
    public List<ProductModel> Products { get; set; }
}