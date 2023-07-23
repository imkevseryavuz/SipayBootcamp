﻿using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }

        private readonly IBookStoreDbContext _dbContext;

        public CreateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidCastException("Kitap zaten mevcut!");

            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;
            book.AuthorId = Model.AuthorId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
