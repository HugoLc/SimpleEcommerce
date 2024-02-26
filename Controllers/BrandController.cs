using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Data;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers;

[ApiController]
[Route("api/")]
public class BrandController : Controller{

    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandController(IBrandRepository brandRepository, IMapper mapper){
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    [HttpGet("v1/brands")]
    public IActionResult Get()
    {
        var brands = _mapper.Map<List<BrandDto>>(_brandRepository.GetBrands());

        if (!ModelState.IsValid)
                return BadRequest(ModelState);

        return Ok(brands);
    }

    [HttpGet("v1/brands/{id:int}")]
    public async Task<IActionResult> Get(
        [FromRoute] int id,
        [FromServices] AppDbContext ctx
    ){
        var brand = _mapper.Map<BrandDto>(_brandRepository.GetBrand(id));

        if(brand == null) 
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(brand);

    }

    [HttpPost("v1/brands")]
    public async Task<IActionResult> Post(
        [FromBody] BrandDto brandCreate
    ){
        if (brandCreate == null)
                return BadRequest(ModelState);

        var brand = _brandRepository.GetBrands()
            .Where(brand=> brand.Name.Trim().ToUpper() == brandCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

        if (brand != null)
        {
            ModelState.AddModelError("", "Country already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var brandMap = _mapper.Map<BrandModel>(brandCreate);
        if(!_brandRepository.CreateBrand(brandMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Created");
    }
}