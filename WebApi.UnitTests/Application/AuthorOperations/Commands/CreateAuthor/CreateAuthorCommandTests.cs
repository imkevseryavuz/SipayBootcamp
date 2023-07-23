
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
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

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
 
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            var author = new Author() { Name = "Kevser", Surname = "Yavuz", Birthday = new System.DateTime(1997, 06, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel() { Name = author.Name };


            // act & asset
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("A author with the same name already exists.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            CreateAuthorModel model = new CreateAuthorModel() { Name = "Kevser", Surname = "Yav", Birthday = new System.DateTime(1997, 06, 10) };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);
            author.Should().NotBeNull();
            author.Birthday.Should().Be((Convert.ToDateTime(model.Birthday)));

        }




    }
}
