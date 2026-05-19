using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Application.DTOs
{
    public class UserStatsDto
    {
        public int TotalBooks { get; set; }
        public int BooksCompleted { get; set; }
        public int BooksInProgress { get; set; }
        public int TotalPagesRead { get; set; }
        public double CompletionPercentage { get; set; }
    }
}
