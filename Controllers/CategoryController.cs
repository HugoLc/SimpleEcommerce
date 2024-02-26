using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Data;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers;

[ApiController]
[Route("api/")]
public class CategoryController : Controller{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper){
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet("v1/categories")]

    public async Task<IActionResult> Get()
    {
        var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

        if (!ModelState.IsValid)
                return BadRequest(ModelState);

        return Ok(categories);
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> Get(
        [FromRoute] int id
    ){
        var cattegory = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

        if(cattegory == null) 
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(cattegory);
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> Post(
        [FromBody] CategoryDto categoryCreate
    ){
        if (categoryCreate == null)
                return BadRequest(ModelState);

        var brand = _categoryRepository.GetCategories()
            .Where(category=> category.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

        if (brand != null)
        {
            ModelState.AddModelError("", "Category already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoryMap = _mapper.Map<CategoryModel>(categoryCreate);
        if(!_categoryRepository.CreateCategory(categoryMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Created");
    }
}