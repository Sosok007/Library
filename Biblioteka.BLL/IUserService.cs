using Biblioteka.Entities;

namespace Biblioteka.BLL;

public interface IUserService
{
    Task<Polzak?> GetUserByCredentialsAsync(string username, string hash);
    Task<Guid> AddUserAsync(Polzak polzak);
    Task DeletUserkAsync(Guid polzakId);
    Task UpdateUserAsync(Guid polzakId, Polzak updatepolzak);
    Task<IReadOnlyCollection<Polzak>> GetPolzaksAsync();
    Task<Polzak?> GetPolzakByIdAsync(Guid id);
}