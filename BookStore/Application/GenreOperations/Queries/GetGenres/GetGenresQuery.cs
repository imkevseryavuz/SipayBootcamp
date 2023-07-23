using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Common;
using BookStore.DBOperations;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _context;
       
        public GetGenresQuery(IBookStoreDbContext context)
        {
            _context = context;  

        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> wm = new List<GenresViewModel>();
            foreach (var genre in genres)
            {
                wm.Add(new GenresViewModel()
                {
                    Id=genre.Id,
                    Name = genre.Name

                });
            }
            return wm;
 

        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
