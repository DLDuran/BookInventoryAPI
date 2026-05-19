using BookInventory.Application.DTOs;
using BookInventory.Domain.Entities;

namespace BookInventory.Application.Mappings
{
    public static class BookMappingExtensions
    {
        public static BookDto ToDto(this Book book)
        {
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Status = book.Status.ToString(),
                InterestLevel = book.InterestLevel,
                TotalPages = book.TotalPages,
                PagesRead = book.PagesRead
            };
        }
    }
}