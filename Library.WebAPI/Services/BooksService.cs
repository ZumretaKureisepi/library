﻿using AutoMapper;
using Library.Model.DTO;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Library.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Services
{
    public interface IBooksService
    {
        public Task<List<BookGetDto>> GetBooksAsync(BookSearchRequest request);
        public Task<BookPaginateGetDto> GetBooksPaginateAsync(BookSearchRequest request);
        public Task<BookGetDto> GetBookById(long id);
        public Task<BookInsertResponse> InsertBookAsync(BookInsertRequest Book);
        public Task<Book> UpdateBookAsync(BookInsertRequest request);
        public Task<BookGetDto> DeleteBookAsync(long id);
    }

    public class BooksService : IBooksService
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public BooksService(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookGetDto>> GetBooksAsync(BookSearchRequest request)
        {
            var booksList = await _context.Books
            .Where(x => request.Title == null || x.Title.ToLower()
            .Contains(request.Title.ToLower())).OrderBy(x => x.BookId).ToListAsync();

            return _mapper.Map<List<BookGetDto>>(booksList);
        }

        public async Task<BookPaginateGetDto> GetBooksPaginateAsync(BookSearchRequest request)
        {
            var query = _context.Books
                .Where(x => request.Title == null || x.Title.ToLower().Contains(request.Title.ToLower())).OrderBy(x => x.BookId);

            var list = await query.ApplyPagination(request).ToListAsync();
            var totalRecords = await query.CountAsync();
            var listMapped = _mapper.Map<List<BookGetDto>>(list);
            var booksPaginate = new BookPaginateGetDto
            {
                Books = listMapped,
                BooksCount = totalRecords
            };

            return booksPaginate;
        }

        public async Task<BookGetDto> GetBookById(long id)
        {
            var bookGetDto = await _context.Books
                .Where(x => x.BookId == id)
                .Include(x => x.AuthBooks)
                .Select(x => new BookGetDto
                {
                    BookId = x.BookId,
                    Description = x.Description,
                    Image = x.Image,
                    Pages = x.Pages,
                    Title=x.Title,
                    Price = x.Price,
                    PublisherId = x.PublisherId,
                    AuthorIds = x.AuthBooks.Select(y => y.AuthorId).ToList()
                })
                .FirstOrDefaultAsync();

            return bookGetDto;
        }

        public async Task<BookInsertResponse> InsertBookAsync(BookInsertRequest Book)
        {
            var bookToInsert = _mapper.Map<Book>(Book);

            _context.Books.Add(bookToInsert);
            await _context.SaveChangesAsync();

            foreach (var item in Book.AuthorIds)
            {
                AuthBook book = new AuthBook
                {
                    BookId = bookToInsert.BookId,
                    AuthorId = item
                };

                _context.AuthBooks.Add(book);

            }
            await _context.SaveChangesAsync();

            return new BookInsertResponse();
        }

        public async Task<Book> UpdateBookAsync(BookInsertRequest request)
        {
            var book =await _context.Books
            .Include(x => x.AuthBooks)
            .FirstOrDefaultAsync(x => x.BookId == request.BookId);

            var authBooksToRemove = book.AuthBooks.Where(x => !request.AuthorIds.Contains(x.AuthorId));

            foreach (var authBook in authBooksToRemove)
            {
                authBook.IsDeleted = true;
            }

            var authorIdsToInsert = request.AuthorIds.Where(x => !book.AuthBooks.Select(y => y.AuthorId).Contains(x));

            foreach (var authorId in authorIdsToInsert)
            {
                book.AuthBooks.Add(new AuthBook
                {
                    Book = book,
                    AuthorId = authorId
                });
            }

            _context.Books.Update(book);
            _mapper.Map(request, book);

            await _context.SaveChangesAsync();

            return _mapper.Map<Book>(book);
        }

        public async Task<BookGetDto> DeleteBookAsync(long id)
        {
            var book =await _context.Books.FindAsync(id);
            if (book == null && book.IsDeleted)
            {
                return null;
            }

            book.IsDeleted = true;
            await _context.SaveChangesAsync();

            return _mapper.Map<BookGetDto>(book);
        }

    }
}
