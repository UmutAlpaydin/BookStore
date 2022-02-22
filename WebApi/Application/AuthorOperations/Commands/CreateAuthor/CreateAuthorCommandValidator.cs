using System;
using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace WebApi.Application.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}