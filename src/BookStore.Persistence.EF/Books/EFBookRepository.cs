using BookStore.Entities;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksRepository = BookStore.Services.Books.Contracts.BooksRepository;

namespace BookStore.Persistence.EF.Books
{
    public class Efcategoryrepository : BooksRepository
    {
        private readonly EFDataContext _dataContext;
        public Efcategoryrepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Book book)
        {
            _dataContext.Books.Add(book);
        }

        public IList<GetBooksDto> GetAll()
        {
            return _dataContext.Books
                .Select(_ => new GetBooksDto
                {
                    Id = _.Id,
                    Description = _.Description,
                    Pages = _.Pages,
                    Title = _.Title,
                    Author = _.Author,
                    CategoryId = _.CategoryId
                }).ToList();
        }

        public void Delete(Book book)
        {
           _dataContext.Books.Remove(book);
        }
        public Book GetBookById(int id)
        {
            return _dataContext.Books.FirstOrDefault(_ => _.Id == id);
        }
    }
}
