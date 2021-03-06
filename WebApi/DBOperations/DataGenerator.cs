using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author{
                        Name = "George",
                        Surname = "Martin",
                        BirthDate =  new System.DateTime(1981,06,12)
                    },
                     new Author{
                        Name = "Test",
                        Surname = "TestName",
                        BirthDate =  new System.DateTime(1962,08,12)
                    },
                     new Author{
                        Name = "Name",
                        Surname = "Surname",
                        BirthDate =  new System.DateTime(1942,10,22)
                    }
                );

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                     new Genre{
                        Name = "Science Fiction"
                    },
                     new Genre{
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                new Book{
                   
                    Title = "Lean Startup",
                    GenreId = 1,
                    AuthorId = 1, //PersonalGrowth
                    PageCount = 200,
                    PublishDate = new System.DateTime(2001,06,12)
                },
                new Book{
                   
                    Title = "Herland",
                    GenreId = 2, //ScienceFiction
                    AuthorId = 2, 
                    PageCount = 250,
                    PublishDate = new System.DateTime(2010,05,23)
                },
                new Book{
        
                    Title = "Dune",
                    GenreId = 2, //ScienceFiction
                    AuthorId = 3, 
                    PageCount = 540,
                    PublishDate = new System.DateTime(2002,05,23)
                } 
                );

                context.SaveChanges();
            }
        }
    }
}