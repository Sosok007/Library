using Biblioteka.Entities;

namespace Biblioteka.BLL;

public interface IBookService
{
    Task<Guid?> InsertBookAsync(Book newBook);
    Task UpdateBookAsync(Guid bookId, Book updatedBook);
    Task<IReadOnlyCollection<Book>> GetBookAsync(); 
    Task<Book?> GetBookByIdAsync(Guid id);
    Task DeleteBookAsync(Guid bookId);
     
}