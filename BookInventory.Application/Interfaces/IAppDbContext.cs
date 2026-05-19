using BookInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}