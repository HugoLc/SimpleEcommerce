using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Request;

public class SkuReqDto{
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