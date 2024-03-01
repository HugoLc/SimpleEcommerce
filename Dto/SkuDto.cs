namespace SimpleEcommerce.Dto;

public class SkuDto{
    public int SkuId { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
}