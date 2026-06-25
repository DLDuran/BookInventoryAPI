using BookInventory.Application.DTOs;
using BookInventory.Application.Interfaces;
using BookInventory.Application.Interfaces.Services;
using BookInventory.Application.Security;
using BookInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookInventory.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;

    public AuthService(IAppDbContext context, ITokenService tokenService, IConfiguration config)
    {
        _context = context;
        _tokenService = tokenService;
        _config = config;
    }

    public async Task<bool> RegisterAsync(RegisterRequest registerDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return false;
        }

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = PasswordHasher.HashPassword(registerDto.Password)
        };

        _context.Users.Add(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
            double.Parse(_config["Jwt:RefreshTokenExpirationDays"] ?? "7"));

        await _context.SaveChangesAsync();

        return new AuthResponse(accessToken, refreshToken);
    }

    public async Task<AuthResponse> RefreshTokenAsync(TokenRequest tokenRequestDto)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequestDto.AccessToken);
        if (principal == null)
        {
            return null;
        }
        var userIdString = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!long.TryParse(userIdString, out long userId))
        {
            return null;
        }
        var user = await _context.Users.FindAsync(userId);

        if (user == null ||
                user.RefreshToken != tokenRequestDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _context.SaveChangesAsync();

        return new AuthResponse(newAccessToken, newRefreshToken);
    }
}