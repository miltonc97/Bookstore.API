using Bookstore.API.Models;
using Bookstore.API.Models.DTOs;
using Bookstore.API.Services;
using Microsoft.AspNetCore.Mvc;
using Supabase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BookService _bookService;

        public OrderController(BookService bookService)
        {
            _bookService = bookService;
        }

        // Reserve a book
        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveBook([FromBody] OrderDto orderDto)
        {
            var success = await _bookService.ReserveBook(orderDto.BookId, orderDto.Quantity);
            if (!success) return BadRequest("Not enough stock available to reserve.");
            return Ok(new { Message = "Book reserved successfully" });
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseBook([FromBody] OrderDto orderDto)
        {
            var success = await _bookService.CompletePurchase(orderDto.BookId, orderDto.Quantity);
            if (!success) return BadRequest("Not enough reserved stock to complete purchase.");
            return Ok(new { Message = "Purchase completed" });
        }
    }

    public class PurchaseRequest
    {
        public int BookId { get; set; }
    }
}

