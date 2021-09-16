using Library.Model.DTO;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorByIdAsync(long id)
        {
            return Ok(await _authorsService.GetAuthorByIdAsync(id));
        }

        [HttpPut]
        public async Task<AuthorsInsertResponse> UpdateAuthorAsync([FromBody] AuthorsInsertRequest request)
        {
            return await _authorsService.UpdateAuthorAsync(request);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAuthorAsync([FromBody] AuthorsInsertRequest request)
        {
            return Ok(await _authorsService.InsertAuthorAsync(request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorGetDto>> DeleteAuthorAsync(long id)
        {
            return await _authorsService.DeleteAuthorAsync(id);
        }


    }
}
