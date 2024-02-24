using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.ViewModels;

public class EditorBrandViewModel{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
}