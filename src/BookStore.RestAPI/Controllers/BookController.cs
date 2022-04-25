using BookStore.Services.Books.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.RestAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;
        public BookController(BookService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddBooksDto dto)
        {
            _service.Add(dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var book = _service.GetBookById(id);
            _service.Delete(book);
        }
    }
}
