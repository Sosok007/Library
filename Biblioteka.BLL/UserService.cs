using System.ComponentModel.DataAnnotations;
using Biblioteka.DAL;
using Biblioteka.Entities;

namespace Biblioteka.BLL;

public class UserService: IUserService 
{
    private readonly IUserRepository _userRepository ;
    private readonly IValidatable<Polzak> _userValidator;

    public UserService(IValidatable<Polzak> userValidator, IUserRepository userRepository)
    {
        _userValidator = userValidator;
        _userRepository = userRepository;
    }

    public async Task<Polzak?> GetUserByCredentialsAsync(string username, string hash)
    {
        return await _userRepository.GetUserByCredentialsAsync(username, hash);
    }
    
    public async Task<Guid> AddUserAsync(Polzak polzak)
    {
        return await _userRepository.AddUserAsync(polzak);
    }

    public async Task DeletUserkAsync(Guid polzakId)
    {
        await _userRepository.DeletUserkAsync(polzakId);
    }

    public async Task UpdateUserAsync(Guid polzakId, Polzak updatepolzak)
    {
            try
            {
                _userValidator.Validate(updatepolzak);
                await _userRepository.UpdateUserAsync(polzakId, updatepolzak);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
    }

    public async Task<IReadOnlyCollection<Polzak>> GetPolzaksAsync() => await _userRepository.GetPolzaksAsync();
    

    public async Task<Polzak?> GetPolzakByIdAsync(Guid id)
    {
        return await _userRepository.GetPolzakByIdAsync(id);
    }
}