using BookStore.Application.GenreOperations.Commands.UpdateGenre;
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

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 0;

            // act & asset 
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aranılan tür bulunamadı.");

        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "Poem" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel() { Name = "Poem" };

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimde  mevcut bir tür var.");
        }

        [Fact]
        public void WhenGivenBookIdinDB_Genre_ShouldBeUpdate()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            UpdateGenreModel model = new UpdateGenreModel() { Name = "WhenGivenBookIdinDB_Genre_ShouldBeUpdate" };
            command.Model = model;
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();

        }
    }
}
