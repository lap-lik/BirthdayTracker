using Core.DTOs.Input.Person;
using Core.DTOs.Output.Person;

namespace Core.Intefaces.Application.Service
{
    public interface IPersonService
    {
        Task<PersonResponse> AddAsync(Guid userId, CreatePersonRequest request);
        Task UpdateAsync(Guid userId, Guid personId, UpdatePersonRequest request);
        Task DeleteAsync(Guid userId, Guid personId);
        Task<PersonResponse> GetPersonAsync(Guid userId, Guid personId);
        Task<IEnumerable<PersonResponse>> GetAllPersonsAsync(Guid userId);
        Task<IEnumerable<PersonResponse>> GetUpcomingBirthdaysAsync(Guid userId);
    }
}