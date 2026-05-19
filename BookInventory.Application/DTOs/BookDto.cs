namespace BookInventory.Application.DTOs;

public class BookDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int InterestLevel { get; set; }
    public int TotalPages { get; set; }
    public int PagesRead { get; set; }
    public string? CoverImagePath { get; set; }
}