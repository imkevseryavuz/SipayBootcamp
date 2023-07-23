using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Command.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(1, "Lor", 1)]
        [InlineData(0, "Lord", 1)]
        [InlineData(1, "Lord O", -1)]
        [InlineData(0, "Lor", 0)]
        [InlineData(-1, "Lord Of", -1)]
        [InlineData(1, " ", 1)]
        [InlineData(1, "", 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookid, string title, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel() { Title = title, GenreId = genreId };
            command.BookId = bookid;
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [InlineData(1, 1, "Lord Of The Rings")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int bookId, int genreId, string title)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId
            };
            command.BookId = bookId;

            UpdateBookCommandValidator validations = new UpdateBookCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

    }
}
