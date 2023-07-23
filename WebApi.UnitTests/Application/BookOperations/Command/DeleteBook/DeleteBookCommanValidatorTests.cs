using BookStore.Application.BookOperations.Commands.DeleteBook;
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
    public class DeleteBookCommanValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int bookid)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null!);
            command.BookId = bookid;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(2)]
        public void WhenInvalidBookIdisGiven_Validator_ShouldNotBeReturnError(int bookid)
        {
            DeleteBookCommand command = new DeleteBookCommand(null!);
            command.BookId = bookid;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);

        }
    }

 }
