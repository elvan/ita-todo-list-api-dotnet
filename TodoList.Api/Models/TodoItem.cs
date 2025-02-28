using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public class TodoItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public int ChecklistId { get; set; }
    public Checklist Checklist { get; set; } = null!;
}
