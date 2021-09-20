using Library.Model.DTO;
using Library.Model.Requests;
using Library.WebAPI.Models;
using Library.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _BooksService;

        public BooksController(IBooksService BooksService)
        {
            _BooksService = BooksService;
        }

        [HttpGet]
        public async Task<List<BookGetDto>> GetBooksAsync([FromQuery] BookSearchRequest Request)
        {
            return await _BooksService.GetBooksAsync(Request);
        }

        [HttpGet("paginate")]
        public async Task<BookPaginateGetDto> GetBooksPaginateAsync([FromQuery] BookSearchRequest request)
        {
            return await _BooksService.GetBooksPaginateAsync(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(long id)
        {
            return Ok(await _BooksService.GetBookById(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertBookAsync([FromBody] BookInsertRequest request)
        {
            return Ok(await _BooksService.InsertBookAsync(request));
        }

        [HttpPut]
        public async Task<Book> UpdateBookAsync([FromBody] BookInsertRequest request)
        {
            return await _BooksService.UpdateBookAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookGetDto>> DeleteBookAsync(long id)
        {
            return await _BooksService.DeleteBookAsync(id);
        }

    }
}
