using BookInventory.Domain.Entities;

namespace BookInventory.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetByUserIdAsync(long userId);
        Task<Book?> GetByIdAsync(int id, long userId);
        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task<bool> SaveChangesAsync();
    }
}