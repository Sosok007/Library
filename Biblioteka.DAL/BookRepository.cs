using System.Runtime.Intrinsics.X86;
using Biblioteka.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid?> InsertBookAsync(Book newBook)
    {
        if (newBook == null)
        {
            throw new ArgumentNullException(nameof(newBook));
        }
        await _context.Books.AddAsync(newBook);
        await _context.SaveChangesAsync();
        return  newBook.Id;
    }
    public async Task DeleteBookAsync(Guid bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateBookAsync(Guid bookId, Book updatedBook)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return;
        book.Id = updatedBook.Id;
        book.Avtors = updatedBook.Avtors;
        book.Name = updatedBook.Name;
        book.ISBN = updatedBook.ISBN;
        book.Created = updatedBook.Created;
        book.City = updatedBook.City;
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Book>> GetBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }
}  