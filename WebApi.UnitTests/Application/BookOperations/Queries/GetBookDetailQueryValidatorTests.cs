using BookStore.Application.BookOperations.Queries.GetBookDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Queries
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-10)]
        [Theory]
        public void WhenInvalidBookidIsGiven_Validator_ShouldBeReturnErrors(int bookid)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookid;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1)]
        [InlineData(100)]
        [Theory]
        public void WhenInvalidBookidIsGiven_Validator_ShouldNotBeReturnErrors(int bookid)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookid;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }


    }
}
