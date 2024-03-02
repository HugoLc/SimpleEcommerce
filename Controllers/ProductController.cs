

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Data;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController: Controller{

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper){
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("v1/products")]
        public async Task<IActionResult> Get()
        {
            var productModels = _productRepository.GetProducts();
            var productDtos = new List<ProductDto>();
            foreach (var product in productModels)
            {
                var prodDto = _mapper.Map<ProductDto>(product);
                prodDto.BrandId = product.Brand.BrandId;
                prodDto.CategoryIds = [];
                prodDto.SkuIds = [];
                foreach (var catprod in product.CategoryProduct)
                {
                    prodDto.CategoryIds.Add(catprod.CategoryId);
                }
                foreach (var sku in product.Skus)
                {
                    prodDto.SkuIds.Add(sku.SkuId);
                }
                productDtos.Add(prodDto);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(productDtos);
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> Get(
            [FromRoute] int id
        ){
            var productModel = _productRepository.GetProduct(id);
            var productDto = _mapper.Map<ProductDto>(productModel);
            productDto.SkuIds = [];
            productDto.CategoryIds = [];

            if(productDto == null) 
                return NotFound();

            foreach (var sku in productModel.Skus)
            {
                productDto.SkuIds.Add(sku.SkuId);
            }
            foreach (var category in productModel.CategoryProduct)
            {
                productDto.CategoryIds.Add(category.CategoryId);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productDto);
        }

         [HttpPost("v1/products")]
        public async Task<IActionResult> Post(
            [FromBody] ProductDto productCreate
        ){
            if (productCreate == null)
                    return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(product=> product.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            var productModel = _mapper.Map<ProductModel>(productCreate);
            if(!_productRepository.CreateProduct(productModel, productCreate.CategoryIds, productCreate.BrandId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Created");
        }
    }
    
}