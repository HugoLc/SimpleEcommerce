using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Request;

public class BrandReqDto{


    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
}