using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {    //DependencyInjection classından geliyor
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;  
                }
                context.Genres.AddRange
                    (
                    new Genre{ Name="Personal Growth",},
                    new Genre{ Name = "Science Fiction",},
                    new Genre{ Name = "Novel",}
                    );
                context.Authors.AddRange
                    (
                    new Author { Name = "Eric", Surname = "Ries", Birthday = new DateTime(1978, 09, 22) },
                    new Author { Name = "Charlotte", Surname = "Perkins Gilman", Birthday = new DateTime(1860, 07, 03) },
                    new Author { Name = "Frank", Surname = "Herbert", Birthday = new DateTime(1920, 10, 08) }
                    );

                    context.Books.AddRange
                    (
                   new Book{Title = "Lean Startup",AuthorId=1,GenreId = 1,PageCount = 200,PublishDate = new DateTime(2001, 06, 12)},
                   new Book{Title = "Herland", AuthorId = 2,GenreId = 2,PageCount = 200,PublishDate = new DateTime(2001, 06, 12)},
                   new Book{Title = "Dune", AuthorId = 3, GenreId = 3,PageCount = 200, PublishDate = new DateTime(2001, 06, 12) }
                    );

                context.SaveChanges();
            }
        }
    }
}
