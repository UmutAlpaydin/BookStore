using System;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command=> command.BookId).GreaterThan(0);
        }
    }
}