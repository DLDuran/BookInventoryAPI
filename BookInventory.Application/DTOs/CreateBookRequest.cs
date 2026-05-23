namespace BookInventory.Application.DTOs
{
    public class CreateBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public int TotalPages { get; set; }
        public int InterestLevel { get; set; } = 1;
        public string? CoverImagePath { get; set; }
        public DateTime? ReadingStaredDate { get; set; }
        public DateTime? DateFinished { get; set; }
    }
}