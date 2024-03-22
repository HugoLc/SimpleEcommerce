using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Attributes;
using SimpleEcommerce.Dto.Request;
using SimpleEcommerce.Dto.Response;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers;

[ApiController]
[Route("api/")]
[ApiKey]

public class SkuController : Controller{
    private readonly ISkuRepository _skuRepository;
    private readonly IMapper _mapper;
    public SkuController(ISkuRepository skuRepository, IMapper mapper){
        _skuRepository = skuRepository;
        _mapper = mapper;
    }

    [HttpGet("v1/skus")]
    public async Task<IActionResult> Get(){

        var skuModels = _skuRepository.GetSkus();
        // var skuDtos = _mapper.Map<List<SkuDto>>(skuModels);
        var skuDtos = new List<SkuResDto>();
        foreach (var sku in skuModels)
        {
            var skuDto = _mapper.Map<SkuResDto>(sku);
            skuDto.ProductId = sku.Product.ProductId;
            skuDtos.Add(skuDto);
        }

        if (!ModelState.IsValid)
                return BadRequest(ModelState);

        return Ok(skuDtos);
    }
    [HttpGet("v1/skus/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var skuModel = _skuRepository.GetSku(id);
        var skuDto = _mapper.Map<SkuResDto>(skuModel);

        if(skuDto == null)
            return NotFound();

        skuDto.ProductId = skuModel.Product.ProductId;

        if (!ModelState.IsValid)
                return BadRequest(ModelState);

        return Ok(skuDto);
    }
    [HttpPost("v1/skus")]
    public async Task<IActionResult> Post(
        [FromBody] SkuReqDto skuCreate
    ){
        if(skuCreate==null){
            return BadRequest(skuCreate);
        }
        var skuFromDb = _skuRepository.GetSkus()
                .Where(product=> product.Name.Trim().ToUpper() == skuCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

        if(skuFromDb != null){
            ModelState.AddModelError("modelError", "Sku already exists");
                return StatusCode(422, ModelState);
        }

        var skuModel = _mapper.Map<SkuModel>(skuCreate);
        var createdSku = _skuRepository.CreateSku(skuModel, skuCreate.ProductId);
        if(createdSku == null)
        {
            ModelState.AddModelError("modelError", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }
        return Ok(new {
            createdSku.SkuId,
            createdSku.Name,
            createdSku.ImageUrl,
            createdSku.Price,
            createdSku.Stock,
            createdSku.Product.ProductId
        });
    }

    [HttpPut("v1/skus/{id:int}")]
    public async Task<IActionResult> Put(
        [FromBody] SkuReqDto skuUpdate,
        [FromRoute] int id
    ){
        if(id == 0 || skuUpdate == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var sku = _mapper.Map<SkuModel>(skuUpdate);
            sku.SkuId = id;
            var finalSku = _skuRepository.UpdateSku(sku);
            return Ok(new {
                skuId = finalSku.SkuId,
                productId = finalSku.Product.ProductId,
                imageUrl = finalSku.ImageUrl,
                name = finalSku.Name,
                price = finalSku.Price,
                stock = finalSku.Stock
            });
        }
        catch (System.Exception)
        {
            
            throw;
        }

    }
    [HttpDelete("v1/skus-delete/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id
    )
    {
        if(id == 0)
            return BadRequest();
        if(!_skuRepository.DeleteSku(id))
        {
            ModelState.AddModelError("modelError", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }
        return Ok("Deleted");
    
    }
}