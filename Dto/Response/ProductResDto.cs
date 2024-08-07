
using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Response
{
    public class ProductResDto
    {
        public int ProductId { get; set; }
        public List<int> SkuIds { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O slug é obrigatório")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "O id da marca é obrigatório")]
        public int BrandId { get; set; }
        
        [Required(ErrorMessage = "Ao menos uma categoria é obrigatória")]
        public List<int> CategoryIds { get; set; }
        
    }
}