using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.UpdateBook;
using Xunit;
using static WebApi.Application.BookOperations.Command.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("lord", 0)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("lor", 1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1, "lord of the rings", 1)]
        [InlineData(1, "DENEME", 2)]

        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int bookId, string title, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }

    }
}