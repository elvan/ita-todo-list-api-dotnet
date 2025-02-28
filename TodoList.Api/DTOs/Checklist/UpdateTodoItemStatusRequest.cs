using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs.Checklist;

public record UpdateTodoItemStatusRequest(
    [Required]
    bool IsCompleted
);
