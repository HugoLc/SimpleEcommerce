

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Attributes;
using SimpleEcommerce.Dto.Request;
using SimpleEcommerce.Dto.Response;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers
{
    [ApiController]
    [Route("api/")]
    [ApiKey]

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
            var productDtos = new List<ProductResDto>();
            foreach (var product in productModels)
            {
                var prodDto = _mapper.Map<ProductResDto>(product);
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
        public async Task<IActionResult> GetById(
            [FromRoute] int id
        ){
            var productModel = _productRepository.GetProductById(id);
            var productDto = _mapper.Map<ProductResDto>(productModel);
            productDto.BrandId = productModel.Brand.BrandId;
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
        [HttpGet("v1/products-by-category/{id:int}")]
        public async Task<IActionResult> GetByCategory(
            [FromRoute] int id 
        ){
            var productModel = _productRepository.GetProductsByCategory(id);
            if(productModel == null)
                return NotFound();
            var productResponse = productModel.Select(p=> new{
                productId = p.ProductId,
                name = p.Name,
                slug = p.Slug,
                brand = new {
                    brandId = p.Brand.BrandId,
                    name = p.Brand.Name
                },
                categoryIds = p.CategoryProduct.Select(cp=>cp.CategoryId),
                skus = p.Skus.Select(s => new {
                        skuId = s.SkuId,
                        name = s.Name,
                        imageUrl = s.ImageUrl,
                        price = s.Price,
                        stock = s.Stock
                    })
                
            });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(productResponse);
        }
        [HttpGet("v1/products-by-brand/{id:int}")]
        public async Task<IActionResult> GetByBrand(
            [FromRoute] int id 
        )
        {
            var productModel = _productRepository.GetProductsByBrand(id);
            if(productModel == null)
                return NotFound();
            var productResponse = productModel.Select(p=> new{
                productId = p.ProductId,
                name = p.Name,
                slug = p.Slug,
                brand = new {
                    brandId = p.Brand.BrandId,
                    name = p.Brand.Name
                },
                categoryIds = p.CategoryProduct.Select(cp=>cp.CategoryId),
                skus = p.Skus.Select(s => new {
                        skuId = s.SkuId,
                        name = s.Name,
                        imageUrl = s.ImageUrl,
                        price = s.Price,
                        stock = s.Stock
                    })
                
            });
            return Ok(productResponse);
        }
        [HttpGet("v1/products-by-slug")]
        public async Task<IActionResult> GetBySlug(
            [FromQuery] string slug 
        ){
            var productModel = _productRepository.GetProductBySlug(slug);
            if(productModel == null)
                return NotFound();
            var productResponse = new{
                productId = productModel.ProductId,
                name = productModel.Name,
                slug = productModel.Slug,
                brand = new {
                    brandId = productModel.Brand.BrandId,
                    name = productModel.Brand.Name
                },
                categoryIds = productModel.CategoryProduct.Select(cp=>cp.CategoryId),
                skus = productModel.Skus.Select(s => new {
                        skuId = s.SkuId,
                        name = s.Name,
                        imageUrl = s.ImageUrl,
                        price = s.Price,
                        stock = s.Stock
                    })
                
            };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(productResponse);
        }

         [HttpPost("v1/products")]
        public async Task<IActionResult> Post(
            [FromBody] ProductReqDto productCreate
        ){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (productCreate == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(product=> product.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("modelError", "Product already exists");
                return StatusCode(422, ModelState);
            }


            var productModel = _mapper.Map<ProductModel>(productCreate);
            List<SkuModel> skus = [];
            productCreate.Skus.ForEach(s=>skus.Add(_mapper.Map<SkuModel>(s)));
            var createdProduct = _productRepository.CreateProduct
                (
                    productModel, 
                    productCreate.CategoryIds, 
                    productCreate.BrandId, 
                    skus
                );
            if(createdProduct==null)
            {
                ModelState.AddModelError("modelError", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new {
                createdProduct.ProductId,
                createdProduct.Name,
                createdProduct.Slug,
                createdProduct.Brand.BrandId,
                categoryIds = createdProduct.CategoryProduct.Select(cp=>cp.CategoryId),
                skus = createdProduct.Skus.Select(s=> new{
                    s.SkuId,
                    s.Name,
                    s.ImageUrl,
                    s.Price,
                    s.Stock
                })
            });
        }
        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> Put(
            [FromRoute] int id,
            [FromBody] ProductUpdateReqDto productUpdate
        )
        {
            try
            {
                var product = _productRepository.UpdateProduct(productUpdate, id);
                return Ok(new {
                    producId = product.ProductId,
                    name = product.Name,
                    slug = product.Slug,
                    brandId = product.Brand.BrandId,
                    categoryIds = product.CategoryProduct.Select(cp=> cp.CategoryId),
                    skuIds = product.Skus.Select(s=> s.SkuId)
                });

            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        [HttpDelete("v1/products-delete/{id:int}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id
        )
        {
            if(id == 0)
                return BadRequest();
            if(!_productRepository.DeleteProduct(id))
            {
                ModelState.AddModelError("modelError", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Deleted");
        }
    }
    
}