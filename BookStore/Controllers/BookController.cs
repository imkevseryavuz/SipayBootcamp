﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount=200,
                PublishDate=new DateTime(2001,06,12)

            },

            new Book
            {
                Id = 2,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount=200,
                PublishDate=new DateTime(2001,06,12)
            },

            new Book
            {
                Id = 3,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount=200,
                PublishDate=new DateTime(2001,06,12)
            }
        };

        //Class içerisindeki verileri listelemeyi sağlar
        /* [HttpGet]
         public List<Book> GetBooks()
         {
             var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
             return bookList;
         }
        */

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpGet]
        public Book Get([FromQuery] int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }


        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)
                return BadRequest();

            BookList.Add(newBook);
            return Ok();

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id ==id);

            if (book is null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount=updatedBook.PageCount!= default ? updatedBook.PageCount: book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;


            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            BookList.Remove(book);
            return Ok();
            
        }
    
    }    
}
