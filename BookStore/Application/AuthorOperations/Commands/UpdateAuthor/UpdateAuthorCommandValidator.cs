﻿using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {

            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(4).When(x => x.Model.Surname != string.Empty);

        }
    }
}
