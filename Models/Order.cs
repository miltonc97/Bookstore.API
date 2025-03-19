namespace Bookstore.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // Pending, Completed, etc.
    }
}
