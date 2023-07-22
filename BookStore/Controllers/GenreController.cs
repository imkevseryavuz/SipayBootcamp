using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public GenreController(BookStoreDbContext context)
        {
            _context = context;


        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GenreDetailViewModel result;
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);

            command.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updatedGenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }


    }
}
