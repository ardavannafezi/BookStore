using BookStore.Entities;
using BookStore.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Books.Contracts
{
    public interface BookService : Service
    {
        void Add(AddBooksDto dto);
        void Delete(Book book);

        IList<GetBooksDto> GetAll();
        Book GetBookById(int id);
    }
}
