using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooksById
{
    public class GetBooksByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Book Handle(int id)
        {
            var book = _dbContext.Books.Where(book=> book.Id == id).SingleOrDefault();
            return book;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }

        public string Genre { get; set; }
    }
}