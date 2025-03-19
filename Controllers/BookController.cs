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
    public class BookController : ControllerBase
    {
        private readonly Client _supabaseClient;
        private readonly BookService _bookService;

        public BookController(SupabaseService supabaseService, BookService bookService)
        {
            _supabaseClient = supabaseService._client;
            _bookService = bookService;
        }

        // GET /<BooksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var response = await _supabaseClient
                .From<Book>()
                .Get();

            return Ok(response.Models);
        }

        // GET /<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == id)
                .Get();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Model);
        }

        // POST /<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] BookDto bookDto)
        {
            var book = Book.FromDto(bookDto);

            var response = await _supabaseClient
                .From<Book>()
                .Insert(book);

            return CreatedAtAction(nameof(GetBook), new { id = response.Models.First().Id }, response.Models.First());
        }

        // PUT /<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] BookDto bookDto)
        {
            // Validate the incoming bookDto
            if (bookDto == null)
            {
                return BadRequest("Invalid book data.");
            }

            // Use the BookService to update the book with the provided data
            var success = await _bookService.UpdateBook(id, bookDto);

            if (!success)
            {
                return NotFound("Book not found.");
            }

            return NoContent();
        }

        // DELETE /<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == id)
                .Delete();

            return NoContent();
        }
    }
}
