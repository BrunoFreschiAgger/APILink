using System.ComponentModel.DataAnnotations;

namespace APILink.ViewModels;

public class CreateTodoViewModel
{
    [Required]
    public string? Title { get; set; }
}