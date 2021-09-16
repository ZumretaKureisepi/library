using AutoMapper;
using Library.Model.DTO;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Models;
using Library.Model.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Services
{

        public interface IPublishersService
        {
            public Task<List<PublisherGetDto>> GetPublishersAsync(PublishersSearchRequest request);
            public Task<PublisherPaginateGetDto> GetPublishersPaginateAsync(PublishersSearchRequest request);
            public Task<PublishersEditGetDto> GetPublisherByIdAsync(long id);
            public Task<PublishersInsertResponse> InsertPublisherAsync(PublishersInsertRequest Publisher);
            public Task<PublishersInsertResponse> UpdatePublisherAsync(PublishersInsertRequest request);
            public Task<PublisherGetDto> DeletePublisherAsync(long id);
        }

        public class PublishersService : IPublishersService
        {
            private readonly LibraryContext _context;
            private readonly IMapper _mapper;

            public PublishersService(LibraryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

        public async Task<List<PublisherGetDto>> GetPublishersAsync(PublishersSearchRequest request)
        {
            var publishersList = await _context.Publishers
            .Where(x => request.Name == null || x.Name.ToLower()
            .Contains(request.Name.ToLower())).Include(x => x.Adress)
            .OrderBy(x => x.PublisherId).ToListAsync();

            return _mapper.Map<List<PublisherGetDto>>(publishersList);
        }

        public async Task<PublisherPaginateGetDto> GetPublishersPaginateAsync(PublishersSearchRequest request)
        {
            var query = _context.Publishers
                .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower())).OrderBy(x => x.PublisherId);

            var list = await query.ApplyPagination(request).ToListAsync();
            var totalRecords = await query.CountAsync();
            var listMapped = _mapper.Map<List<PublisherGetDto>>(list);
            var publishersPaginate = new PublisherPaginateGetDto
            {
                Publishers = listMapped,
                PublishersCount = totalRecords
            };

            return publishersPaginate;
        }

        public async Task<PublishersEditGetDto> GetPublisherByIdAsync(long id)
        {
            var publisherGetDto = await _context.Publishers
                .Where(x => x.PublisherId == id)
                .Include(x => x.Adress)
                .Select(x => new PublishersEditGetDto
                {
                    Name = x.Name,
                    PublisherId = x.PublisherId,
                    AdressId = x.AdressId,
                    ZipCode = x.Adress.ZipCode,
                    City = x.Adress.City,
                    Country = x.Adress.Country,
                    Road = x.Adress.Road

                })
                .FirstOrDefaultAsync();

            return publisherGetDto;
        }

        public async Task<PublishersInsertResponse> InsertPublisherAsync(PublishersInsertRequest Publisher)
        {
            var newAdress = new Adress
            {
                City = Publisher.City,
                Country = Publisher.Country,
                Road = Publisher.Road,
                ZipCode = Publisher.ZipCode
            };

            var newPublisher = new Publisher
            {
                Name = Publisher.Name,
                Adress = newAdress
            };

            _context.Publishers.Add(newPublisher);
            await _context.SaveChangesAsync();

            return _mapper.Map<PublishersInsertResponse>(newPublisher);
        }

        public async Task<PublishersInsertResponse> UpdatePublisherAsync(PublishersInsertRequest request)
        {
            var publisher =await _context.Publishers
            .FirstOrDefaultAsync(x => x.PublisherId == request.PublisherId);

            var adress =await _context.Adresses
            .FirstOrDefaultAsync(x => x.AdressId == request.AdressId);

            _context.Publishers.Update(publisher);
            _context.Adresses.Update(publisher.Adress);
            _mapper.Map(request, publisher);
            _mapper.Map(request, adress);

            await _context.SaveChangesAsync();

            return _mapper.Map<PublishersInsertResponse>(publisher);
        }

        public async Task<PublisherGetDto> DeletePublisherAsync(long id)
        {
            var Publisher =await _context.Publishers.FindAsync(id);
            if (Publisher == null && Publisher.IsDeleted)
            {
                return null;
            }

            Publisher.IsDeleted = true;
            await _context.SaveChangesAsync();

            return _mapper.Map<PublisherGetDto>(Publisher);
         }
    }
}
