using AutoMapper;
using Library.Model.DTO;
using Library.Model.Extensions;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Services
{
    public interface IAuthorsService
    {
        public Task<List<AuthorGetDto>> GetAuthorsAsync(AuthorsSearchRequest request);
        public Task<AuthorPaginateGetDto> GetAuthorsPaginateAsync(AuthorsSearchRequest request);
        public Task<AuthorGetDto> GetAuthorByIdAsync(long id);
        public Task<AuthorsInsertResponse> InsertAuthorAsync(AuthorsInsertRequest Author);
        public Task<AuthorsInsertResponse> UpdateAuthorAsync(AuthorsInsertRequest request);
        public Task<AuthorGetDto> DeleteAuthorAsync(long id);
    }
    public class AuthorsService : IAuthorsService
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public AuthorsService(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AuthorGetDto>> GetAuthorsAsync(AuthorsSearchRequest request)
        {
            var authorsList = await _context.Authors
                .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower())).OrderBy(x => x.AuthorId).ToListAsync();

            return _mapper.Map<List<AuthorGetDto>>(authorsList);
        }

        public async Task<AuthorPaginateGetDto> GetAuthorsPaginateAsync(AuthorsSearchRequest request)
        {
            var query = _context.Authors
                .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower())).OrderBy(x => x.AuthorId);

            var list = await query.ApplyPagination(request).ToListAsync();
            var totalRecords = await query.CountAsync();
            var listMapped=_mapper.Map<List<AuthorGetDto>>(list);

            var authorsPaginate = new AuthorPaginateGetDto
            {
                Authors = listMapped,
                AuthorsCount = totalRecords
            };

            return authorsPaginate;
        }
        public async Task<AuthorGetDto> GetAuthorByIdAsync(long id)
        {
            var authorGetDto = await _context.Authors
                .Where(x => x.AuthorId == id)
                .Include(x => x.AuthBooks)
                .Select(x => new AuthorGetDto
                {
                    Name = x.Name,
                    AuthorId=x.AuthorId,
                    Biography = x.Biography,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    Image=x.Image,
                    BookIds = x.AuthBooks.Select(y => y.BookId).ToList()
                })
                .FirstOrDefaultAsync();

            return authorGetDto;
        }

        public async Task<AuthorsInsertResponse> InsertAuthorAsync(AuthorsInsertRequest Author)
        {
            var authorToInsert = _mapper.Map<Author>(Author);

            _context.Authors.Add(authorToInsert);
            await _context.SaveChangesAsync();

            foreach (var item in Author.BookIds)
            {
                AuthBook authBook = new AuthBook
                {
                    AuthorId = authorToInsert.AuthorId,
                    BookId = item
                };
                _context.AuthBooks.Add(authBook);

            }
            await _context.SaveChangesAsync();

            return new AuthorsInsertResponse();
        }

        public async Task<AuthorsInsertResponse> UpdateAuthorAsync(AuthorsInsertRequest request)
        {
            var author =await _context.Authors
            .Include(x => x.AuthBooks)
            .FirstOrDefaultAsync(x => x.AuthorId == request.AuthorId);

            var authBooksToRemove = author.AuthBooks.Where(x => !request.BookIds.Contains(x.BookId));

            foreach (var authBook in authBooksToRemove)
            {
                authBook.IsDeleted = true;
            }

            var bookIdsToInsert = request.BookIds.Where(x => !author.AuthBooks.Select(y => y.BookId).Contains(x));

            foreach (var bookId in bookIdsToInsert)
            {
                author.AuthBooks.Add(new AuthBook
                {
                    Author = author,
                    BookId = bookId
                });
            }

            _context.Authors.Update(author);
            _mapper.Map(request, author);

            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorsInsertResponse>(author);
        }

        public async Task<AuthorGetDto> DeleteAuthorAsync(long id)
        {
            var author =await _context.Authors.FindAsync(id);

            if (author == null && author.IsDeleted)
            {
                return null;
            }

            author.IsDeleted = true;
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorGetDto>(author);
        }
    }
}
