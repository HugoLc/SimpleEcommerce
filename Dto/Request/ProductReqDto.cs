
using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Dto.Request
{
    public class ProductReqDto
    {
        [Required(ErrorMessage = "O sku é obrigatório")]
        public List<SkuByProdReqDto> Skus { get; set; }
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