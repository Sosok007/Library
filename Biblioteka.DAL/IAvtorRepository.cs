using Biblioteka.Entities;

namespace Biblioteka.DAL;

public interface IAvtorRepository
{
    Task<Avtor?> GetAvtorByIdAsync(Guid id);
    Task<Guid?> InsertAvtorAsync(Avtor newAuthor);
    Task DeleteAvtorAsync(Guid authorId);
    Task UpdateAvtorAsync(Guid authorId, Avtor updatedAuthor);
    Task<IReadOnlyCollection<Avtor>> GetAvtorsAsync();
    Task<IReadOnlyCollection<Avtor>> GetAvtorsByIdsAsync(IEnumerable<Guid> ids);
}