using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Linq;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;

        public CreateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidCastException("Kitap türü  zaten mevcut!");

            genre = new Genre();
            genre.Name = Model.Name;
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();

        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }

    }
}
