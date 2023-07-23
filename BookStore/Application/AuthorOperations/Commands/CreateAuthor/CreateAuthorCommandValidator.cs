using FluentValidation;
using System;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(command => command.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(command => command.Model.Birthday.Date).LessThan(DateTime.Now.Date);
        }
    }
}
