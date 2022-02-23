using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.DeleteBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-50)]

        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]

        public void WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnErrors(int id)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}