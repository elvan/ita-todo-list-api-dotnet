using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs.Checklist;

public record CreateTodoItemRequest(
    [Required]
    [StringLength(200)]
    string Description
);
