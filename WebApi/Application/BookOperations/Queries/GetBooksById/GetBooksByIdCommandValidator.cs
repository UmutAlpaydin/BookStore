using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBooksById{

    public class GetBooksByIdCommandValidator : AbstractValidator<GetBooksByIdQuery>
    {
        public GetBooksByIdCommandValidator()
        {
            RuleFor(query=> query.BookId).GreaterThan(0);
        }
    }
}