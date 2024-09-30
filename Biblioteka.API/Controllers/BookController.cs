using Biblioteka.API.Models;
using Biblioteka.BLL;
using Biblioteka.DAL;
using Biblioteka.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.API.Controllers;


[ApiController]
[Route("[controller]")]
    public class BooksController : ControllerBase
    {
        
        private readonly IBookService _bookService;
        

        public BooksController(IBookService bookService, AppDbContext context)
        {
            _bookService = bookService;
         
        }


       
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookService.GetBookAsync()); 
        }

        
        [HttpGet("poisk")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }

        
        [HttpPost("создание")]
        public async Task<IActionResult> CreateBook([FromBody]CreatBook book)
        {
            var result = await _bookService.InsertBookAsync(new Book
            {
                ISBN = book.ISBN, Publisher = book.Publisher, Created = book.Created, City = book.City,
                 Name = book.Name
            });
            return Ok(result); 
        }

        
        [HttpPost("obnova ")]
        public async Task<IActionResult> UpdateBook([FromRoute]Guid id, [FromBody]Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest("net takoi knigi");
            }

            await _bookService.UpdateBookAsync(id, updatedBook);
            return NoContent();
        }

        
        [HttpDelete("udalaem")]
        public async Task<IActionResult> DeleteBook([FromRoute]Guid id)
        {
            await _bookService.DeleteBookAsync(id); 
            return NoContent();
        }
    }
