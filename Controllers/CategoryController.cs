using BlogApi.Data;
using BlogApi.Extensions;
using BlogApi.Models;
using BlogApi.ViewModels;
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

            return Ok(new ResultViewModel<List<Category>>(categories));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<List<Category>>("05XE05 - Falha interna no servidor"));
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
                return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

            return Ok(new ResultViewModel<Category>(category));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Category>("05XE4 - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> Post(
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        try
        {
            var category = new Category
            {
                Name = model.Name,
                Slug = model.Slug.ToLower(),
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch(DbUpdateConcurrencyException)
        {
            return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível incluir a categoria"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Category>("05XE10 - Falha interna no servidor"));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category =
            await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

            category.Slug = model.Slug;
            category.Name = model.Name;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new ResultViewModel<Category>("05X8 - Não foi possível alterar a categoria"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Category>("05XE11 - Falha interna no servidor"));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var category =
            await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new ResultViewModel<Category>("05X7 - Não foi possível excluir a categoria"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Category>("05XE12 - Falha interna no servidor"));
        }
    }
}
