using Library.Model.DTO;
using Library.Model.Requests;
using Library.Model.Responses;
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
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _PublishersService;

        public PublishersController(IPublishersService PublishersService)
        {
            _PublishersService = PublishersService;
        }

        [HttpGet]
        public async Task<List<PublisherGetDto>> GetPublishersAsync([FromQuery] PublishersSearchRequest Request)
        {
            return await _PublishersService.GetPublishersAsync(Request);
        }

        [HttpGet("paginate")]
        public async Task<PublisherPaginateGetDto> GetPublishersPaginateAsync([FromQuery] PublishersSearchRequest request)
        {
            return await _PublishersService.GetPublishersPaginateAsync(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(long id)
        {
            return Ok(await _PublishersService.GetPublisherById(id));
        }

        [HttpPut]
        public async Task<PublishersInsertResponse> Update([FromBody] PublishersInsertRequest request)
        {
            return await _PublishersService.Update(request);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublisherGetDto>> DeletePublisher(long id)
        {
            return await _PublishersService.DeletePublisher(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPublisher([FromBody] PublishersInsertRequest request)
        {
            return Ok(await _PublishersService.InsertPublisher(request));
        }







    }
}
