using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs.Checklist;

public record UpdateTodoItemRequest(
    [Required]
    [StringLength(200)]
    string Description
);
