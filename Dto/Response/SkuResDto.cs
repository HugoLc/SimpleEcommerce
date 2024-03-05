using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Response;

public class SkuResDto{
    public int SkuId { get; set; }
    public int ProductId { get; set; }
    [Required(ErrorMessage = "A imagem é obrigatória")]
    public string ImageUrl { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O preço é obrigatório")]
    public float Price { get; set; }
    [Required(ErrorMessage = "O estoque é obrigatório")]
    public int Stock { get; set; }
}