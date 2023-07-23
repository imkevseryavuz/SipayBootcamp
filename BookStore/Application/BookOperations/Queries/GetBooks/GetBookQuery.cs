using BookStore.Common;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BookStore.Application.BookOperations.Queries.GetBooks
{
    public class GetBookQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x=>x.Genre).Include(a=>a.Author).OrderBy(x => x.Id).ToList();
            List<BooksViewModel> wm = _mapper.Map< List<BooksViewModel>>(bookList);   
            return wm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }
}
