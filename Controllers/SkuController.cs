using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Dto.Request;
using SimpleEcommerce.Dto.Response;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers;

[ApiController]
[Route("api/")]
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
            ModelState.AddModelError("", "Sku already exists");
                return StatusCode(422, ModelState);
        }

        var skuModel = _mapper.Map<SkuModel>(skuCreate);
        if(!_skuRepository.CreateSku(skuModel, skuCreate.ProductId))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }
        return Ok("Created");
    }
}