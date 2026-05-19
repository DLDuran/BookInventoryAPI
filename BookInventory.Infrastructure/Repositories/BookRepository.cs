using BookInventory.Application.Interfaces.Repositories;
using BookInventory.Domain.Entities;
using BookInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetByUserIdAsync(long userId)
    {
        return await _context.Books
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id, long userId)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public void Update(Book book)
    {
        _context.Books.Update(book);
    }

    public void Delete(Book book)
    {
        _context.Books.Remove(book);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}