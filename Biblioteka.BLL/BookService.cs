using Biblioteka.DAL;
using Biblioteka.Entities;

namespace Biblioteka.BLL;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidatable<Book> _bookValidator;

    public BookService(IValidatable<Book> bookValidator, IBookRepository bookRepository)
    {
        _bookValidator = bookValidator;
        _bookRepository = bookRepository;
    }

        public async Task UpdateBookAsync(Guid bookId, Book updatedBook)
    {
        try
        {
            _bookValidator.Validate(updatedBook);
            await _bookRepository.UpdateBookAsync(bookId, updatedBook);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<IReadOnlyCollection<Book>> GetBookAsync() => await _bookRepository.GetBooksAsync();
    
    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await _bookRepository.GetBookByIdAsync(id);
    }

    public async Task DeleteBookAsync(Guid bookId)
    {
        await _bookRepository.DeleteBookAsync(bookId);
    }


    public async Task<Guid?> InsertBookAsync(Book newBook)
    {
        try
        {
            _bookValidator.Validate(newBook);
            return await _bookRepository.InsertBookAsync(newBook);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}