using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;

        public GetGenreDetailQuery(IBookStoreDbContext context)
        {
            _context = context;

        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id==GenreId);
            GenreDetailViewModel wm = new GenreDetailViewModel();
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            wm.Id = GenreId;
            wm.Name = genre.Name;
            return wm;


        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

