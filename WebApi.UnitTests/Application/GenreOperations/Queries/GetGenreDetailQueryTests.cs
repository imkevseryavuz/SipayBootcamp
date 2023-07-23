using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DBOperations;
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
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;


        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context);
            command.GenreId = 0;


            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Aranılan tür bulunamadı.");
        }

        [Fact]
        public void WhenGivenGenreIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context);
            command.GenreId = 1;


            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Books.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
        }
    }
}
