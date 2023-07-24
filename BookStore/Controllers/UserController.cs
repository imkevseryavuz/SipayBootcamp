using BookStore.Application.UserOperaions.Commands.CreateToken;
using BookStore.Application.UserOperaions.Commands.CreateUser;
using BookStore.Application.UserOperaions.Commands.RefreshToken;
using BookStore.DBOperations;
using BookStore.TokenOperations.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_configuration);
            command.Model=login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token; 
            var resultToken = command.Handle();
            return resultToken;
        }
    }
}