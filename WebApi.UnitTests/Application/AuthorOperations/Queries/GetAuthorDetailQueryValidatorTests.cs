using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-10)]
        [Theory]
        public void WhenInvalidAuthoridIsGiven_Validator_ShouldBeReturnErrors(int authorid)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null);
            query.AuthorId = authorid;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1)]
        [InlineData(100)]
        [Theory]
        public void WhenInvalidAuthoridIsGiven_Validator_ShouldNotBeReturnErrors(int authorid)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null);
            query.AuthorId = authorid;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }


    }
}
