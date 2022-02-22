using FluentValidation;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(4).When(x=> x.Model.Name.Trim() != string.Empty);
        }
    }
}