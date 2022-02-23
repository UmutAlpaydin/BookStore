using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Command.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Command.DeleteBook.DeleteBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void  WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            int bookId = 10;
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            //act && assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Silinecek kitap bulunamadı.");

        }

        [Theory]
        [InlineData(1)]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted(int bookId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == bookId);
            book.Should().Be(null);
        }

    }
}