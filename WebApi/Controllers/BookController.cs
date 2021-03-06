using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Command.CreateBook;
using WebApi.Application.BookOperations.Command.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBooksById;
using WebApi.Application.BookOperations.Command.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Command.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Command.UpdateBook.UpdateBookCommand;
using FluentValidation;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;


        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
          GetBooksQuery query = new GetBooksQuery(_context, _mapper);
          var result = query.Handle();
          return Ok(result);
        }


        [HttpGet("{id}")]
         public IActionResult GetByID(int id)
        {
            BooksByIdViewModel result;
         
            GetBooksByIdQuery query = new GetBooksByIdQuery(_context, _mapper);
            query.BookId = id;
            GetBooksByIdCommandValidator validator = new GetBooksByIdCommandValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        //Post

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel  updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
           
            command.BookId = id;
             command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
          
            return Ok();
        }

        //Delete

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
           
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        
            return Ok();

        }

    }
}