using Biblioteka.Entities;

namespace Biblioteka.DAL;

public interface IBookRepository
{
    Task<Book?> GetBookByIdAsync(Guid id);
    Task<Guid?> InsertBookAsync(Book newBook);
    Task DeleteBookAsync(Guid bookId);
    Task UpdateBookAsync(Guid bookId, Book updatedBook);
    Task<IReadOnlyCollection<Book>> GetBooksAsync(); 
}
