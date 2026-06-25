using BookInventory.Application.DTOs;
using BookInventory.Application.Interfaces.Repositories;
using BookInventory.Application.Interfaces.Services;

namespace BookInventory.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileDto?> GetProfileAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}