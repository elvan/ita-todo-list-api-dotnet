using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs.Auth;

public record RegisterRequest(
    [Required]
    [StringLength(50, MinimumLength = 3)]
    string Username,

    [Required]
    [StringLength(100, MinimumLength = 6)]
    string Password
);
