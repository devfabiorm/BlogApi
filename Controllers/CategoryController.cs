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
        try
        {
            var categories = await context.Categories.ToListAsync();

            return Ok(categories);
        }
        catch (Exception)
        {

            return StatusCode(500, "05XE05 - Falha interna no servidor");
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category =
           await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }
        catch (Exception)
        {

            return StatusCode(500, "05XE4 - Falha interna no servidor");
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> Post(
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
    {

        try
        {
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{model.Id}", model);
        }
        catch(DbUpdateConcurrencyException)
        {
            return StatusCode(500, "05XE9 - Não foi possível incluir a categoria");
        }
        catch (Exception)
        {

            return StatusCode(500, "05XE10 - Falha interna no servidor");
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
    {
        try
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
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, "05X8 - Não foi possível alterar a categoria");
        }
        catch (Exception)
        {

            return StatusCode(500, "05XE11 - Falha interna no servidor");
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category =
            await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, "05X7 - Não foi possível excluir a categoria");
        }
        catch (Exception)
        {

            return StatusCode(500, "05XE12 - Falha interna no servidor");
        }
    }
}
