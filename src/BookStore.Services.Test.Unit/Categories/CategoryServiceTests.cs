﻿using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Categories;
using BookStore.Services.Categories;
using BookStore.Services.Categories.Contracts;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Services.Test.Unit.Categories
{
    public class CategoryServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly CategoryService _sut;
        private readonly CategoryRepository _repository;
        
        public CategoryServiceTests()
        {
            _dataContext =
                new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFCategoryRepository(_dataContext);
            _sut = new CategoryAppService(_repository,_unitOfWork);
        }

        [Fact]
        public void Add_adds_category_properly()
        {
            AddCategoryDto dto = GenerateAddCategoryDto();

            _sut.Add(dto);

            _dataContext.Categories.Should()
                .Contain(_ => _.Title == dto.Title);
        }

        [Fact]
        public void GetAll_returns_all_categories()
        {
            CreateCategoriesInDataBase();

            var expected = _sut.GetAll();

            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.Title == "dummy1");
            expected.Should().Contain(_ => _.Title == "dummy2");
            expected.Should().Contain(_ => _.Title == "dummy3");
        }

        private void CreateCategoriesInDataBase()
        {
            var categories = new List<Category>
            {
                new Category { Title = "dummy1"},
                new Category { Title = "dummy2"},
                new Category { Title = "dummy3"}
            };
            _dataContext.Manipulate(_ =>
            _.Categories.AddRange(categories));
        }

        private static AddCategoryDto GenerateAddCategoryDto()
        {
            return new AddCategoryDto
            {
                Title = "dummy",
               
            };
        }


        [Fact]
        public void Remove_removes_full_category_properly()
        {

            Category category = new Category { Title = "dummy" };
            _dataContext.Manipulate(_ =>
                        _.Categories.AddRange(category));
            _sut.Delete(category);
            _dataContext.Categories.Should()
                .NotContain(_ => _.Id == category.Id);
        }



    }
}
