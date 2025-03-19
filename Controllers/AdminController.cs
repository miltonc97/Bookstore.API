using Bookstore.API.Models.DTOs;
using Bookstore.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly BookService _bookService;

        public AdminController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("sell-instore")]
        public async Task<IActionResult> SellBookInStore([FromBody] OrderDto orderDto)
        {
            var success = await _bookService.SellBookInStore(orderDto.BookId, orderDto.Quantity);
            if (!success) return BadRequest("Not enough stock to sell.");
            return Ok(new { Message = "Book sold in-store" });
        }

        [HttpPut("update-stock")]
        public async Task<IActionResult> UpdateStock([FromBody] StockUpdateDto stockDto)
        {
            var success = await _bookService.UpdateStock(stockDto.BookId, stockDto.NewStock);
            if (!success) return NotFound("Book not found.");
            return Ok(new { Message = "Stock updated" });
        }
    }
}
