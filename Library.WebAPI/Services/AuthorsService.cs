using AutoMapper;
using Library.Model.DTO;
using Library.Model.Filter;
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
        public Task<AuthorsInsertResponse> InsertAuthor(AuthorsInsertRequest Author);
        public Task<AuthorGetDto> DeleteAuthor(long id);
        public Task<AuthorGetDto> GetAuthorByIdAsync(long id);
        public Task<AuthorsInsertResponse> Update(AuthorsInsertRequest request);
        //public List<AuthorGetDto> GetAuthorsByBookId(AuthorsSearchRequest request);

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
            var list = await _context.Authors
                .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower())).OrderBy(x => x.AuthorId).ToListAsync();

            return _mapper.Map<List<AuthorGetDto>>(list);
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

        public async Task<AuthorsInsertResponse> Update(AuthorsInsertRequest request)
        {
            var author =await _context.Authors
       .Include(x => x.AuthBooks)
       .FirstOrDefaultAsync(x => x.AuthorId == request.AuthorId);

            var authBooksToRemove = author.AuthBooks.Where(x => !request.BookIds.Contains(x.BookId));

            foreach (var book in authBooksToRemove)
            {
                book.IsDeleted = true;
            }
            /*ako se medju id-ijevima autora iz baze ne nalazi id autora iz requesta onda se autor dodaje*/
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
        //public List<AuthorGetDto> GetAuthorsByBookId(AuthorsSearchRequest request)
        //{

        //    var list = _context.AuthBooks.Where(a => a.BookId == request.BookId).Include(x => x.Author)
        //            .ToList();
        //    var authorsList = new List<Author>();
        //    if (list != null)
        //    {
        //        //query = query.Where(x => (x.FirstName + " " + x.LastName).Contains(request.Name));
        //        foreach (var item in list)
        //        {
        //            authorsList.Add(item.Author);

        //        }
        //    }

        //    return _mapper.Map<List<AuthorGetDto>>(authorsList);
        //}

        public async Task<AuthorsInsertResponse> InsertAuthor(AuthorsInsertRequest Author)
        {
            var newAuthor = _mapper.Map<Author>(Author);

            

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            foreach (var item in Author.BookIds)
            {
                AuthBook authB = new AuthBook
                {
                    AuthorId = newAuthor.AuthorId,
                    BookId = item
                };
                _context.AuthBooks.Add(authB);

            }
            await _context.SaveChangesAsync();


            return new AuthorsInsertResponse();

            //return _mapper.Map<BooksInsertResponse>(newBook);
        }

        public async Task<AuthorGetDto> DeleteAuthor(long id)
        {
            var Author =await _context.Authors.FindAsync(id);
            if (Author == null && Author.IsDeleted)
            {
                return null;
            }

            Author.IsDeleted = true;
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorGetDto>(Author);
        }


    }
}
