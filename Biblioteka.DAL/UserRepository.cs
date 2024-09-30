using Azure.Core;
using Biblioteka.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Polzak?> GetUserByCredentialsAsync(string username, string hash)
    {
        return await _context.Polzaks.FirstOrDefaultAsync(x => x.Username == username && x.Password == hash); 
    }

    public async Task<Guid> AddUserAsync(Polzak polzak)
    {
         await _context.Polzaks.AddAsync(polzak);
         await _context.SaveChangesAsync();
         return polzak.Id; 
    }

    public async Task DeletUserkAsync(Guid polzakId)
    {
        var polzak = await _context.Polzaks.FindAsync(polzakId);
        if (polzak == null) return;

        _context.Polzaks.Remove(polzak);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateUserAsync(Guid polzakId, Polzak updatepolzak)
    {
        var polzak = await _context.Polzaks.FirstOrDefaultAsync(x => x.Id == polzakId);
        if (polzak != null)
        {
            polzak.Username = updatepolzak.Username;
            polzak.Password = updatepolzak.Password;
            polzak.Role = updatepolzak.Role; 
            _context.Polzaks.Update(polzak);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<IReadOnlyCollection<Polzak>> GetPolzaksAsync() => await _context.Polzaks.ToListAsync();


    public async Task<Polzak?> GetPolzakByIdAsync(Guid id)
    {
        return await _context.Polzaks.FirstOrDefaultAsync(x => x.Id == id);
    }
}