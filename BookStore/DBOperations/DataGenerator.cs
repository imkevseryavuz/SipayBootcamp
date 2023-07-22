﻿using BookStore.Entities;
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
                context.Genres.AddRange(
                    new Genre
                    {
                       Name="Personal Growth",
                       
                    },
                    new Genre
                    {
                        Name = "Science Fiction",

                    },
                    new Genre
                    {
                        Name = "Novel",

                    });

                    context.Books.AddRange(
                   new Book
                   {
                       Title = "Lean Startup",
                       GenreId = 1,
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book
                   {
                 
                       Title = "Herland",
                       GenreId = 2,
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book
                   {
                 
                       Title = "Dune",
                       GenreId = 3,
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   }
                   );

                context.SaveChanges();
            }
        }
    }
}
