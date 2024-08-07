using APILink.Data;
using APILink.Models;
using APILink.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILink.Controllers;

[Route("v1")]
[ApiController]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> Get([FromServices] AppDbContext context)
    {
        List<Todo> todos = await context.Todos.AsNoTracking().ToListAsync(); //todo = tarefa

        return Ok(todos);
    }

    [HttpGet]
    [Route("todos/{id}")]
    public async Task<IActionResult> GetById([FromServices] AppDbContext context, [FromRoute] int id)
    {
        Todo? todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost("todos")]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Todo todo = new()
        {
            Title = model.Title,
            Done = false,
            Date = DateTime.Now,
        };

        try
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();

            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch (Exception e) { return BadRequest(e.Message); }
    }

    [HttpPut("todos/{id}")]
    public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Todo? todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (todo == null) return NotFound();

        try
        {
            todo.Title = model.Title;

            context.Todos.Update(todo);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e) { return BadRequest(e.Message); }
    }

    [HttpDelete("todos/{id}")]
    public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
        Todo? todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

        try
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e) { return BadRequest(e.Message); }
    }
}