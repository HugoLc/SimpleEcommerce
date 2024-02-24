using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Data;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers;

[ApiController]
[Route("api/")]
public class BrandController : ControllerBase{
    [HttpGet("v1/brands")]
    public async Task<IActionResult> Get(
        [FromServices] AppDbContext ctx
    )=> Ok(ctx.Brands.ToList());

    [HttpGet("v1/brands/{id:int}")]
    public async Task<IActionResult> Get(
        [FromRoute] int id,
        [FromServices] AppDbContext ctx
    ){
        var brands = ctx.Brands.FirstOrDefault(brand => brand.BrandId == id);
            if (brands == null)
                return NotFound();
            return Ok(brands);
    }

    [HttpPost("v1/brand")]
    public async Task<IActionResult> Post(
        [FromBody] BrandModel brand,
        [FromServices] AppDbContext ctx
    ){
        ctx.Brands.Add(brand);
        ctx.SaveChanges();

        return Created($"v1/{brand.BrandId}",brand);
    }
}