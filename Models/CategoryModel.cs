namespace SimpleEcommerce.Models;

public class CategoryModel{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<CategoryProductModel> CategoryProduct { get; set; }
}