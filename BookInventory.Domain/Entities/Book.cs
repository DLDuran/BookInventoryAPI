using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookInventory.Domain.Enums;

namespace BookInventory.Domain.Entities
{
    public class Book:BaseEntity
    {

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        public string? CoverImagePath { get; set; }

        public ReadingStatus Status { get; set; } = ReadingStatus.NotStarted;

        public int InterestLevel { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PagesRead { get; set; }

        // PostgreSQL Date types
        public DateOnly? ReadingStartedDate { get; set; }
        public DateOnly? DateFinished { get; set; }

        // Foreign Key changed from int to Guid to match the new User Id
        [Required]
        public long UserId { get; set; }

        // Navigation property for the relationship with User
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}