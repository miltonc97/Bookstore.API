using Bookstore.API.Models;
using Bookstore.API.Models.DTOs;
using Supabase;

namespace Bookstore.API.Services
{
    public class BookService
    {
        private readonly Client _supabaseClient;

        public BookService(SupabaseService supabaseService)
        {
            _supabaseClient = supabaseService._client;
        }

        // Update a book
        public async Task<bool> UpdateBook(int id, BookDto bookDto)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == id)
                .Single();

            if (response == null) return false;

            var book = response;

            // Update fields only if they're provided in the DTO
            if (bookDto.Title != null) book.Title = bookDto.Title;
            if (bookDto.Author != null) book.Author = bookDto.Author;
            if (bookDto.Genre != null) book.Genre = bookDto.Genre;
            if (bookDto.ISBN != null) book.ISBN = bookDto.ISBN;
            if (bookDto.Price != null) book.Price = bookDto.Price;
            if (bookDto.Stock.HasValue) book.Stock = bookDto.Stock;
            if (bookDto.ReleaseDate.HasValue) book.ReleaseDate = bookDto.ReleaseDate;

            await _supabaseClient.From<Book>().Update(book);
            return true;
        }

        // Reserve books
        public async Task<bool> ReserveBook(int bookId, int quantity)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == bookId)
                .Single();

            if (response == null) return false;

            var book = response;
            if (book.Stock - book.ReservedStock < quantity) return false;

            book.ReservedStock += quantity;
            await _supabaseClient.From<Book>().Update(book);
            return true;
        }

        // Complete a purchase
        public async Task<bool> CompletePurchase(int bookId, int quantity)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == bookId)
                .Single();

            if (response == null) return false;

            var book = response;
            if (book.ReservedStock < quantity) return false;

            book.ReservedStock -= quantity;
            book.Stock -= quantity;

            await _supabaseClient.From<Book>().Update(book);
            return true;
        }

        // Sell in-store
        public async Task<bool> SellBookInStore(int bookId, int quantity)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == bookId)
                .Single();

            if (response == null) return false;

            var book = response;
            if (book.Stock < quantity) return false;

            book.Stock -= quantity;
            await _supabaseClient.From<Book>().Update(book);
            return true;
        }

        // Update stock manually
        public async Task<bool> UpdateStock(int bookId, int newStock)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(x => x.Id == bookId)
                .Single();

            if (response == null) return false;

            var book = response;
            book.Stock = newStock;

            await _supabaseClient.From<Book>().Update(book);
            return true;
        }
    }
}
