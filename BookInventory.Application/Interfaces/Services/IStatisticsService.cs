using BookInventory.Application.DTOs;
using BookInventory.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Application.Interfaces.Services
{
    public interface IStatisticsService
    {
        Task<UserStatsDto> GetUserSummaryAsync(long userId);
    }
}
