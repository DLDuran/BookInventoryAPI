using BookInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Application.DTOs
{
    public class UpdateBookRequest
    {
        public string? Title { get; set; } 
        public string? Author { get; set; } 
        public ReadingStatus? Status { get; set; } 
        public int? InterestLevel { get; set; }
        public int? TotalPages { get; set; }
        public int? PagesRead { get; set; }
        public string? CoverImagePath { get; set; }
    }
}
