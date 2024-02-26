using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto;

public class BrandDto{

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
}