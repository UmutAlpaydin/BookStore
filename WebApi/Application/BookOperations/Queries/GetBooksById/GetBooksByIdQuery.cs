using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBooksById
{
    public class GetBooksByIdQuery
    {
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBooksByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BooksByIdViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=> x.Genre).Include(x=> x.Author).Where(book=> book.Id == BookId).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Kitap Bulunamadi");

            BooksByIdViewModel vm = _mapper.Map<BooksByIdViewModel>(book);
          
            return vm;
        }
    }

    public class BooksByIdViewModel
    {
        public string Title { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }
    }
}