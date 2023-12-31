﻿using BookService.DTO;
using BookService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAll()
        {
            return await _bookService.GetAllBooks();
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            return await _bookService.GetBook(id);
        }

        [Authorize]
        [HttpGet("of-author/{id:int}")]
        public async Task<ActionResult<AuthorWithBooksDTO>> GetBookOfAuthor(int id)
        {
            return await _bookService.GetBooksByAuthorsId(id);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<ActionResult<BookDTO>> Create([FromForm] CreateBookDTO newBook)
        {
            var ret = await _bookService.CreateBook(newBook);
            return Ok(ret);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<BookDTO>> Update(int id, [FromForm] EditBookDTO newBookInfo)
        {
            var ret = await _bookService.UpdateBook(id, newBookInfo);
            return Ok(ret);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("check/{id:int}")]
        public async Task<ActionResult> CheckBook(int id)
        {
            await _bookService.GetBook(id);
            return Ok();
        }
    }
}
