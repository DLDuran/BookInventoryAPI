using BookInventory.Application.DTOs;
using BookInventory.Application.Interfaces.Repositories;
using BookInventory.Application.Interfaces.Services;
using BookInventory.Domain.Entities;
using BookInventory.Domain.Enums;
using Microsoft.Extensions.Logging;
using BookInventory.Application.Mappings;
using BookInventory.Application.Exceptions;

namespace BookInventory.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<BookDto> CreateBookAsync(CreateBookRequest request, long userId)
        {
            var newBook = new Book
            {
                Title = request.Title,
                Author = request.Author,
                TotalPages = request.TotalPages,
                InterestLevel = request.InterestLevel,
                UserId = userId,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                Status = ReadingStatus.NotStarted,
                CoverImagePath = request.CoverImagePath
            };

            await _bookRepository.AddAsync(newBook);
            await _bookRepository.SaveChangesAsync();

            return newBook.ToDto();
        }

        public async Task<bool> DeleteBookAsync(int id, long userId)
        {
            var book = await _bookRepository.GetByIdAsync(id, userId);

            if (book == null)
            {
                return false;
            }

            _bookRepository.Delete(book);
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id, long userId)
        {
            var book = await _bookRepository.GetByIdAsync(id, userId);

            return book != null ? book.ToDto() : null;
        }

        public async Task<IEnumerable<BookDto>> GetUserBooksAsync(long userId)
        {
            var books = await _bookRepository.GetByUserIdAsync(userId);
            return books.Select(b => b.ToDto());
        }

        public async Task<bool> PatchBookAsync(int id, UpdateBookRequest dto, long userId)
        {
            var book = await _bookRepository.GetByIdAsync(id, userId);

            if (book == null)
            {
                throw new NotFoundException("Book not found.");
            }

            if (book.UserId != userId)
            {
                throw new UnauthorizedAccessException("You don't have permission to modify this book.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                book.Title = dto.Title;
            }

            if (dto.Author != null)
            {
                book.Author = dto.Author;
            }

            if (dto.CoverImagePath != null)
            {
                book.CoverImagePath = dto.CoverImagePath;
            }

            if (dto.Status.HasValue)
            {
                book.Status = dto.Status.Value;
            }

            if (dto.InterestLevel.HasValue)
            {
                book.InterestLevel = dto.InterestLevel.Value;
            }

            if (dto.TotalPages.HasValue)
            {
                book.TotalPages = dto.TotalPages.Value;
            }

            if (dto.PagesRead.HasValue)
            {
                book.PagesRead = dto.PagesRead.Value;
            }

            if (dto.ReadingStaredDate.HasValue)
            {
                book.ReadingStartedDate = DateOnly.FromDateTime(dto.ReadingStaredDate.Value);
            }

            else
            {
                book.ReadingStartedDate = (dto.PagesRead > 0 ? DateOnly.FromDateTime(DateTime.UtcNow) : null);
            }

            if (book.PagesRead >= book.TotalPages && book.TotalPages > 0)
            {
                book.DateFinished = DateOnly.FromDateTime(DateTime.UtcNow);
            }

            if (book.PagesRead > book.TotalPages)
            {
                throw new ArgumentException("Pages read cannot be greater than total pages.");
            }

            _bookRepository.Update(book);
            return await _bookRepository.SaveChangesAsync();
        }
    }
}