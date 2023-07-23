using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            DeleteBookCommand command = new DeleteBookCommand(_context);


            // act and asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            var book = new Book() { Title = "Lord Of The Rings", PageCount = 100, PublishDate = new System.DateTime(1990, 05, 22), GenreId = 1, AuthorId = 1 };
            _context.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            book = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            book.Should().BeNull();
        }
    }
}
