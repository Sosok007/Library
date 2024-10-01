using Biblioteka.DAL;
using Biblioteka.Entities;

namespace Biblioteka.BLL;

public class AvtorService : IAvtorService

{
    private readonly IAvtorRepository _avtorRepository;
    private readonly IValidatable<Avtor> _avtorValidator;

    public AvtorService(IValidatable<Avtor> avtorValidator, IAvtorRepository avtorRepository)
    {
        _avtorValidator = avtorValidator;
        _avtorRepository = avtorRepository;
    }

    public async Task UpdateAvtorAsync(Guid avtorId, Avtor updatedAvtor)
    {
        try
        {
            _avtorValidator.Validate(updatedAvtor);
            await _avtorRepository.UpdateAvtorAsync(avtorId, updatedAvtor);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<Avtor?> GetAvtorByIdAsync(Guid id)
    {
       return await _avtorRepository.GetAvtorByIdAsync(id);
    }

    public async Task DeleteAvtorAsync(Guid authorId)
    {
        await _avtorRepository.DeleteAvtorAsync(authorId);
    }
    

    public async Task<IReadOnlyCollection<Avtor>> GetAvtorsAsync() => await _avtorRepository.GetAvtorsAsync();
    

    public async Task<Guid?> InsertAvtorAsync(Avtor newAvtor)
    {
        try
        {
            _avtorValidator.Validate(newAvtor);
            return await _avtorRepository.InsertAvtorAsync(newAvtor);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<IReadOnlyCollection<Avtor>> GetAuthorsByIdsAsync(IEnumerable<Guid> ids)
    {
        return await _avtorRepository.GetAvtorsByIdsAsync(ids);
    }
}