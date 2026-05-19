using BookInventory.Application.DTOs;

namespace BookInventory.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetUserBooksAsync(long userId);

        Task<BookDto?> GetBookByIdAsync(int id, long userId);

        Task<BookDto> CreateBookAsync(CreateBookRequest request, long userId);

        Task<bool> PatchBookAsync(int id, UpdateBookRequest request, long userId);

        Task<bool> DeleteBookAsync(int id, long userId);
    }
}