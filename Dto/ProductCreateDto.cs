
using System.ComponentModel.DataAnnotations;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Dto
{
    public class ProductCreateDto
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "O sku é obrigatório")]
        public List<SkuDto> Skus { get; set; }
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