using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Biblioteka.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.PL.Controllers;

public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public BookController(ILogger<BookController> logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksAsync()
    {
        var response = await _clientFactory.CreateClient("Base").GetAsync("Book/get-books");
        var books = await response.Content.ReadAsStringAsync();
        var viewModel = JsonSerializer.Deserialize<List<BookViewModel>>(books);
        return View(viewModel);
    }

    // [HttpGet]
    // public async Task<IActionResult> GetBookAsync(Guid id)
    // {
    //     var request = await _clientFactory.CreateClient("libraryApi").GetAsync($"Book/get-book?id={id}");
    // }

    [HttpGet]
    public async Task<IActionResult> AddBookAsync()
    {
        var response = await _clientFactory.CreateClient("Base").GetAsync("Author/authors");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var authors = JsonSerializer.Deserialize<List<AuthorViewModel>>(json);
            
            var bookVm = new BookViewModel
            {
                Authors = authors!
            };
            
            return View(bookVm);
        }

        return View("Error");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBookAsync(BookViewModel book)
    {
        var json = JsonSerializer.Serialize(book);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _clientFactory
            .CreateClient("Base")
            .PostAsync("Book/add-book", payload);

        var responseContent = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetBooks");
        }

        ViewBag.ErrorMessage = responseContent;
        return View("Error");
    
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}