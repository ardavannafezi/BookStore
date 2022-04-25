using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BooksRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        
            public BookAppService(
                BooksRepository repository ,
                UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Add(AddBooksDto dto)
        {
            var book = new Book
            {
                Description = dto.Description,
                Pages = dto.Pages,
                Title = dto.Title,
                Author = dto.Author,
                CategoryId = dto.CategoryId
            };

            _repository.Add(book);

            _unitOfWork.Commit();
        }

        public void Delete(Book book)
        {
            _repository.Delete(book);

            _unitOfWork.Commit();
        }

        public IList<GetBooksDto> GetAll()
        {
            return _repository.GetAll();
        }

        public Book GetBookById(int id)
        {
            return _repository.GetBookById(id);
        }
    }
}
