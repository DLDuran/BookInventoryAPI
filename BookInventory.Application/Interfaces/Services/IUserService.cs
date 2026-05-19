using BookInventory.Application.DTOs;

namespace BookInventory.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetProfileAsync(long userId);
    }
}