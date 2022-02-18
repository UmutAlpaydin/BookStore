using FluentValidation;

namespace WebApi.BookOperations.GetBooksById{

    public class GetBooksByIdCommandValidator : AbstractValidator<GetBooksByIdQuery>
    {
        public GetBooksByIdCommandValidator()
        {
            RuleFor(command=> command.BookId).GreaterThan(0);
        }
    }
}