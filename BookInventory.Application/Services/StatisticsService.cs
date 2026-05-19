using BookInventory.Application.DTOs;
using BookInventory.Application.Interfaces.Repositories;
using BookInventory.Application.Interfaces.Services;
using BookInventory.Application.Mappings;
using BookInventory.Domain.Entities;
using BookInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IBookRepository _bookRepository;
        public StatisticsService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<UserStatsDto> GetUserSummaryAsync(long userId)
        {
            var books = await _bookRepository.GetByUserIdAsync(userId);

            if (books == null || !books.Any())
                return new UserStatsDto();

            var stats = new UserStatsDto
            {
                TotalBooks = books.Count(), 
                BooksCompleted = books.Count(b => b.Status == ReadingStatus.Finished),
                BooksInProgress = books.Count(b => b.Status == ReadingStatus.Reading),
                TotalPagesRead = books.Sum(b => b.PagesRead),
                CompletionPercentage = Math.Round(books.Average(b =>
                    b.TotalPages > 0 ? (double)b.PagesRead / b.TotalPages * 100 : 0), 2)
            };

            return stats; 
        }
    }
}
