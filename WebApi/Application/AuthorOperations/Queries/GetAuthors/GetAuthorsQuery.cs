using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;
        public GetAuthorsQuery(IMapper mapper, BookStoreDbContext context)
        {

            _mapper = mapper;
            _context = context;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x=> x.Id).ToList<Author>();
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
            return returnObj;
        }
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string BirthDate { get; set; }

    }
}