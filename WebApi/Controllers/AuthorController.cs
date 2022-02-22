using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Command.CreateBook;
using WebApi.DBOperations;
using FluentValidation;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Command.CreateAuthor;
using WebApi.Application.AuthorOperations.Command.DeleteAuthor;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;


        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_mapper, _context);
            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpGet("id")]
         public IActionResult GetByID(int id)
        {
         
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }

        //Post

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newBook)
        {
                CreateAuthorCommand command = new CreateAuthorCommand(_context);
                command.Model = newBook;
                CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateAuthorModel  updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
           
            command.AuthorId = id;
            command.Model = updatedAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
          
            return Ok();
        }

        //Delete

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
           
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        
            return Ok();

        }

    }
}