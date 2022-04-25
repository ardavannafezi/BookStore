using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Books.Contracts
{
    public interface BooksRepository : Repository
    {
        void Add(Book book);
        IList<GetBooksDto> GetAll();
        void Delete(Book book);
        public Book GetBookById(int id);
    }
}
