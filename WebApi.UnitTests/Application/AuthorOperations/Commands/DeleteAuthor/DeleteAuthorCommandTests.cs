using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
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

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>


    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);


            // act and asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            var author = new Author() { Name = "Franz", Surname = "Kafka", Birthday = new DateTime(1950, 05, 02) };
            _context.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = author.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            author = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
            author.Should().BeNull();
        }
    }
}
