using APILink.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace APILink.Controllers;

[Route("v1")]
[ApiController]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> Get([FromServices] AppDbContext context)
    {
        var todos = await context.Todos.AsNoTracking().ToListAsync();
        return Ok(todos);
    }

    [HttpGet]
    [Route("todos/{id}")]
    public async Task<IActionResult> GetById([FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return todo == null ? NotFound() : Ok(todo);
    }
}