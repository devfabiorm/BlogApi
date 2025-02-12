using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync(
        [FromServices] BlogDataContext context)
    {
        var categories = await context.Categories.ToListAsync();

        return Ok(categories);
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        var category =
            await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> Post(
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
    {

        await context.Categories.AddAsync(model);
        await context.SaveChangesAsync();

        return Created($"v1/categories/{model.Id}", model);
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
    {
        var category =
            await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound();

        category.Slug = model.Slug;
        category.Name = model.Name;

        context.Categories.Update(category);
        await context.SaveChangesAsync();

        return Ok(model);
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        var category =
            await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound();

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return Ok();
    }
}
