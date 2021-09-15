using Library.Model.DTO;
using Library.Model.Requests;
using Library.WebAPI.Models;
using Library.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<BookGetDto>> GetBooksAsync([FromQuery]BooksSearchRequest Request)
        {
            return await _BooksService.GetBooksAsync(Request);
        }

        [HttpGet("paginate")]
        public async Task<BookPaginateGetDto> GetBooksPaginateAsync([FromQuery] BooksSearchRequest request)
        {
            return await _BooksService.GetBooksPaginateAsync(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(long id)
        {
            return Ok(await _BooksService.GetBookById(id));
        }
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookGetDto>> DeleteBook(long id)
        {
            return await _BooksService.DeleteBook(id);
        }

        [HttpPut]
        public async Task<Book> UpdateAsync([FromBody] BooksInsertRequest request)
        {
            return await _BooksService.UpdateAsync(request);
        }

        [HttpPost]
        public async Task<IActionResult> InsertBook([FromBody] BooksInsertRequest request)
        {
            return Ok(await _BooksService.InsertBook(request));
        }



    }
}
