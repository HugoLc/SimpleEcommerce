using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Data;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Dto.Request;
using SimpleEcommerce.Dto.Response;
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
        var categories = _mapper.Map<List<CategoryResDto>>(_categoryRepository.GetCategories());

        if (!ModelState.IsValid)
                return BadRequest(ModelState);

        return Ok(categories);
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> Get(
        [FromRoute] int id
    ){
        var cattegory = _mapper.Map<CategoryResDto>(_categoryRepository.GetCategory(id));

        if(cattegory == null) 
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(cattegory);
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> Post(
        [FromBody] CategoryReqDto categoryCreate
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

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] CategoryReqDto categoryUpdate
    )
    {
        if(id == 0 || categoryUpdate == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var category = _mapper.Map<CategoryModel>(categoryUpdate);
        category.CategoryId = id;
        try
        {
            var finalCategory = _categoryRepository.UpdateCategory(category);
            return Ok(new {
                categoryId = finalCategory.CategoryId,
                name = finalCategory.Name
            });
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
