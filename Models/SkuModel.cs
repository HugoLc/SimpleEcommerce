namespace SimpleEcommerce.Models;

public class SkuModel{
    public int SkuId { get; set; }
    public ProductModel Product { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
}