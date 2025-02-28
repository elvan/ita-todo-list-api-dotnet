namespace TodoList.Api.DTOs.Checklist;

public record ChecklistResponse(
    int Id,
    string Title,
    IEnumerable<TodoItemResponse> Items
);
