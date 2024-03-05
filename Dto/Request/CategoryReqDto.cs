using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Request
{
    public class CategoryReqDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }
    }
}