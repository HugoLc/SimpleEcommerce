using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Response;

public class BrandResDto{

    public int BrandId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
}