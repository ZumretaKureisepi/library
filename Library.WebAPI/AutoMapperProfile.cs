using AutoMapper;
using Library.Model.DTO;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Models;

namespace Library.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookGetDto>();
            CreateMap<Book, BooksInsertResponse>();
            CreateMap<BooksInsertRequest, Book>();
            CreateMap<Author, AuthorGetDto>();
            CreateMap<Author, AuthorsInsertResponse>();
            CreateMap<AuthorsInsertRequest, Author>();
            CreateMap<Publisher, PublisherGetDto>();
            CreateMap<Publisher, PublishersInsertResponse>();
            CreateMap<PublishersInsertRequest, Publisher>();
            CreateMap<PublishersInsertRequest, Adress>();
            CreateMap<Adress, AdressGetDto>();
        }
    }
}
