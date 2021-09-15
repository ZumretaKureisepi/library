using Library.Model.DTO;
using Library.Model.Filter;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Models;
using Library.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<List<AuthorGetDto>> GetAuthorsAsync([FromQuery] AuthorsSearchRequest request)
        {
            return await _authorsService.GetAuthorsAsync(request);
        }

        [HttpGet("paginate")]
        public async Task<AuthorPaginateGetDto> GetAuthorsPaginateAsync([FromQuery] AuthorsSearchRequest request)
        {
            return await _authorsService.GetAuthorsPaginateAsync(request);
        }

        //[HttpGet("search")]
        //public ActionResult<List<AuthorGetDto>> GetAuthorsByBookId([FromQuery] AuthorsSearchRequest Request)
        //{
        //    return _authorsService.GetAuthorsByBookId(Request);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorByIdAsync(long id)
        {
            return Ok(await _authorsService.GetAuthorByIdAsync(id));
        }

        [HttpPut]
        public async Task<AuthorsInsertResponse> Update([FromBody] AuthorsInsertRequest request)
        {
            return await _authorsService.Update(request);
        }


        [HttpPost]
        public async Task<IActionResult> InsertAuthor([FromBody] AuthorsInsertRequest request)
        {
            return Ok(await _authorsService.InsertAuthor(request));
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorGetDto>> DeleteAuthor(long id)
        {
            return await _authorsService.DeleteAuthor(id);
        }


    }
}
