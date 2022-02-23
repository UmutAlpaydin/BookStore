using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBooksById;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Command.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksByIdViewModel>()
            .ForMember(dest=> dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name))
            .ForMember(dest=> dest.Author, opt=>opt.MapFrom(src=>  src.Author.Name + " " + src.Author.Surname));

            CreateMap<Book, BooksViewModel>()
            .ForMember(dest=> dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name))
            .ForMember(dest=> dest.Author, opt=>opt.MapFrom(src=> src.Author.Name + " " + src.Author.Surname));

            // CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
    }
}