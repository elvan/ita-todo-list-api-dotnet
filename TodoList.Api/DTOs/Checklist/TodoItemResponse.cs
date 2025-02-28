namespace TodoList.Api.DTOs.Checklist;

public record TodoItemResponse(
    int Id,
    string Description,
    bool IsCompleted
);
