using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.UpdateBook;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Application.BookOperations.Command.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            int bookID = 10;

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookID;

            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı.");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            //arrange
            int bookId = 2;
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Hobbit",
                GenreId = 2
            };

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);

            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);

        }
    }
}