using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Entities;

namespace BookStore.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));


        }
    }
}
