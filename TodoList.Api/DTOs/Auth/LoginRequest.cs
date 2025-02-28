using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs.Auth;

public record LoginRequest(
    [Required] string Username,
    [Required] string Password
);
