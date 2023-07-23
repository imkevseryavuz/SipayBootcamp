using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
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
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(" ", " ")]
        [InlineData(" ", "abc")]
        [InlineData("abc", " ")]
        [InlineData("ab", "a")]
        [InlineData("a", "ab")]
        [InlineData("a", "b")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel() { Name = firstname, Surname = lastname, Birthday = new System.DateTime(1900, 01, 25) };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null!);
            command.Model = new CreateAuthorModel()
            {
                Name = "Franz",
                Surname = "Kafka",
                Birthday = DateTime.Now.Date

            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData("abcd ", " abcd")]
        [InlineData("abcd", "abcd")]
        [InlineData("ab  ", "ab  ")]
        [InlineData(" ab ", " a  ")]
        [InlineData("abcdefghıijk", "abcdefghıijk")]
        [InlineData(" aaa", "bbb ")]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel() { Name = firstname, Surname = lastname, Birthday = new System.DateTime(1900, 01, 25) };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}
