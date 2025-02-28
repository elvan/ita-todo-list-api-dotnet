namespace TodoList.Api.DTOs.Auth;

public record AuthResponse(
    string Token,
    string Username
);
