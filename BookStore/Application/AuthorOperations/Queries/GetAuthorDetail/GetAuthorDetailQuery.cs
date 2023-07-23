using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;

        public GetAuthorDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            AuthorDetailViewModel wm = new AuthorDetailViewModel();
            if (author is null)
            {
                throw new InvalidOperationException("Aranılan yazar bulunamadı");
            }
           
            wm.Name = author.Name;
            wm.Surname = author.Surname;
            wm.Birthday = author.Birthday.Date.ToString();
            return wm;


        }
    }
    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}
