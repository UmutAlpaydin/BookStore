using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetBooksById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using FluentValidation;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;


        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id = 1,
        //         Title = "Lean Startup",
        //         GenreId = 1, //PersonalGrowth
        //         PageCount = 200,
        //         PublishDate = new System.DateTime(2001,06,12)
        //     },
        //     new Book{
        //         Id = 2,
        //         Title = "Herland",
        //         GenreId = 2, //ScienceFiction
        //         PageCount = 250,
        //         PublishDate = new System.DateTime(2010,05,23)
        //     },
        //     new Book{
        //         Id = 3,
        //         Title = "Dune",
        //         GenreId = 2, //ScienceFiction
        //         PageCount = 540,
        //         PublishDate = new System.DateTime(2002,05,23)
        //     }
        // };

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
            try{
                 GetBooksByIdQuery query = new GetBooksByIdQuery(_context, _mapper);
                    query.BookId = id;
                    GetBooksByIdCommandValidator validator = new GetBooksByIdCommandValidator();
                    validator.ValidateAndThrow(query);
                    result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


        // [HttpGet]
        //  public Book Get([FromQuery] string id)
        // {
        //     var book = BookList.Where(book=> book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        //Post

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try{
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                // if(!result.IsValid){
                //     foreach(var item in result.Errors)
                //     {
                //         Console.WriteLine("Property " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
                //     }
                // }
                // else{
                // }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel  updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try{
            command.BookId = id;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            command.Model = updatedBook;
            validator.ValidateAndThrow(command);
            command.Handle();
            }
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
            return Ok();
        }

        //Delete

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
           try{
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }

           return Ok();

        }

    }
}