using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-10)]
        [Theory]
        public void WhenInvalidGenreidIsGiven_Validator_ShouldBeReturnErrors(int genreid)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null);
            query.GenreId = genreid;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1)]
        [InlineData(100)]
        [Theory]
        public void WhenInvalidGenreidIsGiven_Validator_ShouldNotBeReturnErrors(int genreid)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null);
            query.GenreId = genreid;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }


    }
}
