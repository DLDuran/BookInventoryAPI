using BookInventory.Application.DTOs;

namespace BookInventory.Application.Interfaces.Services;

public interface IAuthService
{
    // Handles new user creation
    Task<bool> RegisterAsync(RegisterRequest registerDto);

    // Validates credentials and returns tokens
    Task<AuthResponse> LoginAsync(LoginRequest loginDto);

    // Validates refresh token to issue new access tokens
    Task<AuthResponse> RefreshTokenAsync(TokenRequest tokenRequestDto);
}