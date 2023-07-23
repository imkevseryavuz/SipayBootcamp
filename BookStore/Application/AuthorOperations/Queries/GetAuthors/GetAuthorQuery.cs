using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorQuery
    {
        private readonly IBookStoreDbContext _context;


        public GetAuthorQuery(IBookStoreDbContext context)
        {
            _context = context;
        }
        public List<AuthorsViewModel> Handle()
        {
  
                var authors = _context.Authors.OrderBy(x => x.Id);
                List<AuthorsViewModel> wm = new List<AuthorsViewModel>();
                foreach (var author in authors)
                {
                    wm.Add(new AuthorsViewModel()
                    {

                        Name = author.Name,
                        Surname=author.Surname,
                        Birthday=author.Birthday.Date.ToString()

                    });
                }
                return wm;

        }
    }
    public class AuthorsViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Birthday { get; set; }
	}
}
