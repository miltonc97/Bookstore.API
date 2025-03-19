namespace Bookstore.API.Models.DTOs
{
    public class StockUpdateDto
    {
        public int BookId { get; set; }
        public int NewStock { get; set; }
    }
}
