using SimpleEcommerce.Models;

public class CategoryProductModel
{
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
    
    public int CategoryId { get; set; }
    public CategoryModel Category { get; set; }
}