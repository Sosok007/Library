using Biblioteka.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL;

public class AvtorRepository : IAvtorRepository
{
    private readonly AppDbContext _context;

    public AvtorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAvtorAsync(Guid avtorId)
    {
        var avtor = await _context.Avtors
            .FirstOrDefaultAsync(x => x.Id == avtorId);
        if (avtor == null) return;

        _context.Avtors.Remove(avtor);
        await _context.SaveChangesAsync();
    }

    public async Task<Avtor?> GetAvtorByIdAsync(Guid id)
    {
        return await _context.Avtors
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid?> InsertAvtorAsync(Avtor newAvtor)
    {
        ArgumentNullException.ThrowIfNull(newAvtor);
        await _context.Avtors.AddAsync(newAvtor);
        await _context.SaveChangesAsync();
        return newAvtor.Id;
    }

    public async Task UpdateAvtorAsync(Guid id, Avtor updatedAuthor)
    {
        var avtor = await _context.Avtors.FirstOrDefaultAsync(x => x.Id == id);
        if (avtor != null)
        {
            avtor.Firstname = updatedAuthor.Firstname;
            avtor.Patronymic = updatedAuthor.Patronymic;
            avtor.Lastname = updatedAuthor.Lastname;
            avtor.Books = updatedAuthor.Books;

            _context.Avtors.Update(avtor);
        }
    }

    public async Task<IReadOnlyCollection<Avtor>> GetAvtorsAsync() => await _context.Avtors
        .Include(x => x.Books)
        .ToListAsync();
    
    public async Task<IReadOnlyCollection<Avtor>> GetAvtorsByIdsAsync(IEnumerable<Guid> ids)
    {
        return await _context.Avtors.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}    