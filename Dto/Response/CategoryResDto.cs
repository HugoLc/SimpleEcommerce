using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Response
{
    public class CategoryResDto
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }
    }
}