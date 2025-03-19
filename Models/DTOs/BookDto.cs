namespace Bookstore.API.Models.DTOs
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? ISBN { get; set; }
        public string? Price { get; set; }
        public int? Stock { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
