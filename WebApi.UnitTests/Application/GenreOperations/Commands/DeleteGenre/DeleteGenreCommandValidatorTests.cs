using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnErrors(int genreid)
        {
           
            DeleteGenreCommand command = new DeleteGenreCommand(null!);
            command.GenreId = genreid;

         
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

     
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(200)]
        [InlineData(2)]
        public void WhenInvalidGenreIdisGiven_Validator_ShouldNotBeReturnError(int genreid)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null!);
            command.GenreId = genreid;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);

        }

    }
}
