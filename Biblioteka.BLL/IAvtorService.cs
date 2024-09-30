using Biblioteka.Entities;

namespace Biblioteka.BLL;

public interface IAvtorService
{
    Task<Guid?> InsertAvtorAsync(Avtor newAuthor);
    Task UpdateAvtorAsync(Guid authorId, Avtor updatedAuthor);
    
    Task<Avtor?> GetAvtorByIdAsync(Guid id);
    
    Task DeleteAvtorAsync(Guid authorId);
   
    Task<IReadOnlyCollection<Avtor>> GetAvtorsAsync(); 
    
}
