using AutoMapper;
using Library.Model.DTO;
using Library.Model.Requests;
using Library.Model.Responses;
using Library.WebAPI.Models;
using Library.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookGetDto>();
            CreateMap<Book, BooksInsertResponse>();
            CreateMap<BooksInsertRequest, Book >();
            CreateMap<Author, AuthorGetDto >();
            CreateMap<Author, AuthorsInsertResponse >();
            CreateMap<AuthorsInsertRequest, Author >();
            CreateMap<Publisher, PublisherGetDto >();
            CreateMap<Publisher, PublishersInsertResponse>();
            CreateMap<PublishersInsertRequest, Publisher>();
            CreateMap<Adress, AdressGetDto >();
            CreateMap<PublishersInsertRequest, Adress>();



        }





    }
}
