using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly IBookStoreDbContext _context;
        public AuthorController(IBookStoreDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorQuery query = new GetAuthorQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context);

            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updatedAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }
    }
}
