using BookInventory.Application.DTOs;
using BookInventory.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookInventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require authentication by default
    public class BooksController : ApiControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetUserBooksAsync(CurrentUserId);
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
        {
            var books = await _bookService.CreateBookAsync(request, CurrentUserId);
            return Ok(books);
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var books = await _bookService.DeleteBookAsync(id, CurrentUserId);
            return NoContent();
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var books = await _bookService.GetBookByIdAsync(id, CurrentUserId);
            return Ok(books);
        }

        //PATCH: api/books/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook(int id, [FromBody] UpdateBookRequest dto)
        {
            await _bookService.PatchBookAsync(id, dto, CurrentUserId);
            return NoContent();
        }
    }
}