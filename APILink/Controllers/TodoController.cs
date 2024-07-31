using APILink.Data;
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
        var todos = await context.Todos.AsNoTracking().ToListAsync();
        return Ok(todos);
    }
}