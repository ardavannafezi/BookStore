using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Books;
using BookStore.Persistence.EF.Categories;
using BookStore.Services.Books;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Categories;
using BookStore.Services.Categories.Contracts;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Services.Test.Unit.Books
{
    public class BookSerivicesTest
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookService _sut;

        public EFCategoryRepository EFCategoryRepository { get; }

        private readonly CategoryAppService _categorySut;
        private readonly BooksRepository _repository;
        
        public BookSerivicesTest()
        {
            _dataContext =
                new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new Efcategoryrepository(_dataContext);
            _sut = new BookAppService(_repository,_unitOfWork);

            EFCategoryRepository = new EFCategoryRepository(_dataContext);
            _categorySut = new CategoryAppService(EFCategoryRepository, _unitOfWork);


        }

        [Fact]
        public void Add_adds_Book_properly()
        {
            AddBooksDto book = GenerateAddBooksDto();

            AddCategoryDto category = GenerateAddCategoryDto();
            _categorySut.Add(category);

            _sut.Add(book);

            _dataContext.Books.Should()
                .Contain(_ => _.Title == book.Title);
        }


        private static AddCategoryDto GenerateAddCategoryDto()
        {
            return new AddCategoryDto
            {
                Title = "dummy",

            };
        }



        [Fact]
        public void GetAll_returns_all_Books()
        {

            AddCategoryDto category = GenerateAddCategoryDto();
            _categorySut.Add(category);

            CreateABookInDataBase();

            var expected = _sut.GetAll();

            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.Title == "dummy1");
            expected.Should().Contain(_ => _.Title == "dummy2");
            expected.Should().Contain(_ => _.Title == "dummy3");

        }

        private void CreateABookInDataBase()
        {
            var books = new List<Book>
            {
                new Book {
                        Title = "dummy1",
                        Author = "dsdff",
                        Description = "asdas sads",
                        Pages = 6,
                        CategoryId = 1,
                },
                new Book {
                        Title = "dummy2",
                        Author = "man",
                        Description = "asdas sads",
                        Pages = 8,
                        CategoryId = 1,
                },
                new Book {
                        Title = "dummy3",
                        Author = "man",
                        Description = "asdas sads",
                        Pages = 9,
                        CategoryId = 1,
                        }
            };
            _dataContext.Manipulate(_ =>
                _.Books.AddRange(books));
        }


        private static AddBooksDto GenerateAddBooksDto()
        {
            return new AddBooksDto
            {
                
                Title = "dummy",
                Author = "man",
                Description = "asdas sads",
                Pages = 5,
                CategoryId = 1,
            };
        }


        //[Fact]
        //public void Remove_removes_full_category_properly()
        //{

        //    Category category = new Category { Title = "dummy" };
        //    _dataContext.Manipulate(_ =>
        //                _.Categories.AddRange(category));
        //    _sut.Delete(category);
        //    _dataContext.Categories.Should()
        //        .NotContain(_ => _.Id == category.Id);
        //}



    }

}
