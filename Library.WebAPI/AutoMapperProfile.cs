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
            CreateMap<Book, BookInsertResponse>();
            CreateMap<BookInsertRequest, Book>();
            CreateMap<Author, AuthorGetDto>();
            CreateMap<Author, AuthorInsertResponse>();
            CreateMap<AuthorInsertRequest, Author>();
            CreateMap<Publisher, PublisherGetDto>();
            CreateMap<Publisher, PublisherInsertResponse>();
            CreateMap<PublisherInsertRequest, Publisher>();
            CreateMap<PublisherInsertRequest, Adress>();
            CreateMap<Adress, AdressGetDto>();
        }
    }
}
