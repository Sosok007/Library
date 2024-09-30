using Biblioteka.Entities;

namespace Biblioteka.DAL;

public interface IUserRepository
{
    Task<Polzak?> GetUserByCredentialsAsync(string username, string hash);
    Task<Guid> AddUserAsync(Polzak polzak); 
    Task DeletUserkAsync(Guid polzakId);
    Task UpdateUserAsync(Guid polzakId, Polzak updatepolzak);
    Task<IReadOnlyCollection<Polzak>> GetPolzaksAsync();
    Task<Polzak?> GetPolzakByIdAsync(Guid id);
}