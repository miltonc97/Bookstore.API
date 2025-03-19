using Bookstore.API.Models.DTOs;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace Bookstore.API.Models
{
    [Table("book")]
    public class Book : BaseModel
    {
        [PrimaryKey("id")]
        [JsonIgnore]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("genre")]
        public string Genre { get; set; }
        [Column("isbn")]
        public string ISBN { get; set; }
        [Column("price")]
        public string? Price { get; set; }
        [Column("stock")]
        public int? Stock { get; set; }
        [Column("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [Column("reserved_stock")]
        public int ReservedStock { get; set; } = 0;

        public static Book FromDto(BookDto dto)
        {
            return new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
                ISBN = dto.ISBN,
                Price = dto.Price,
                Stock = dto.Stock,
                ReleaseDate = dto.ReleaseDate
            };
        }
    }
}
