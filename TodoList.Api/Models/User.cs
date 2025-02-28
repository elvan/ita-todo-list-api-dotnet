using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(256)]
    public string PasswordHash { get; set; } = string.Empty;

    public List<Checklist> Checklists { get; set; } = new();
}
