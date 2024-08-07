using APILink.Data;
using APILink.Models;
using APILink.ViewModels;

namespace APILink.Services;

public interface ITodoRepository
{
    Task<List<Todo>> GetAsync();
    Task<Todo> GetByIdAsync(int id);
    Task<Todo> PostAsync(AppDbContext context, CreateTodoViewModel model);
    Task<Todo> PutAsync(AppDbContext context, CreateTodoViewModel model, int id);
    Task<Todo> DeleteAsync(AppDbContext context, int id);
}