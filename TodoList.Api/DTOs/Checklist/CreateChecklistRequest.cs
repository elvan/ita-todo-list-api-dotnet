using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs.Checklist;

public record CreateChecklistRequest(
    [Required]
    [StringLength(100)]
    string Title
);
