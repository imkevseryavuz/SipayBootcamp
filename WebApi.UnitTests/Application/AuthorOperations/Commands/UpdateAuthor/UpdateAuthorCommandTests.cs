using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange 
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 0;

            // act and asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");

        }

        [Fact]
        public void WhenGivenAuthorIdinDB_Author_ShouldBeUpdate()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            UpdateAuthorModel model = new UpdateAuthorModel() { Name = "WhenGivenAuthorIdinDB_Author_ShouldBeUpdate", Surname = "Kafka" };
            command.Model = model;
            command.AuthorId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().NotBeNull();

        }
    }
}
