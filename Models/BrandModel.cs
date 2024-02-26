namespace SimpleEcommerce.Models;

public class BrandModel{
    public int BrandId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductModel> Products { get; set; }
}