﻿namespace BookStore.Services.Books.Contracts
{
    public class GetBooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
    }
}