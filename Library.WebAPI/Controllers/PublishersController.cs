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
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _PublishersService;

        public PublishersController(IPublishersService PublishersService)
        {
            _PublishersService = PublishersService;
        }

        [HttpGet]
        public async Task<List<PublisherGetDto>> GetPublishersAsync([FromQuery] PublisherSearchRequest Request)
        {
            return await _PublishersService.GetPublishersAsync(Request);
        }

        [HttpGet("paginate")]
        public async Task<PublisherPaginateGetDto> GetPublishersPaginateAsync([FromQuery] PublisherSearchRequest request)
        {
            return await _PublishersService.GetPublishersPaginateAsync(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherByIdAsync(long id)
        {
            return Ok(await _PublishersService.GetPublisherByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertPublisherAsync([FromBody] PublisherInsertRequest request)
        {
            return Ok(await _PublishersService.InsertPublisherAsync(request));
        }

        [HttpPut]
        public async Task<PublisherInsertResponse> UpdatePublisherAsync([FromBody] PublisherInsertRequest request)
        {
            return await _PublishersService.UpdatePublisherAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PublisherGetDto>> DeletePublisherAsync(long id)
        {
            return await _PublishersService.DeletePublisherAsync(id);
        }

    }
}
