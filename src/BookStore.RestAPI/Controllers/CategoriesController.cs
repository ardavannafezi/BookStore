﻿using BookStore.Services.Categories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.RestAPI.Controllers
{
    [Route("api/catgeories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;
        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddCategoryDto dto)
        {
            _service.Add(dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = _service.GetCategoryById(id);
            _service.Delete(category);
        }
    }
}
