using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public class Checklist
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public List<TodoItem> Items { get; set; } = new();
}
