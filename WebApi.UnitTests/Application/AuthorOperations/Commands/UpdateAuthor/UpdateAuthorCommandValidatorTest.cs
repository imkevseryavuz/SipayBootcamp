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
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0, "Fra", "Kaf")]
        [InlineData(0, "Fra ", "Kafk ")]
        [InlineData(1, "Fran", " ka")]
        [InlineData(0, "Fra", "KAFK")]
        [InlineData(-1, "Franz ", " ")]
        [InlineData(1, " ", " ")]
        [InlineData(1, "", "ASD")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int authorId, string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel() { Name = name, Surname = surname };
            command.AuthorId = authorId;

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [InlineData(1, "Franz", "Kafka")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int authorId, string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname
            };
            command.AuthorId = authorId;

            UpdateAuthorCommandValidator validations = new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }


    }
}
