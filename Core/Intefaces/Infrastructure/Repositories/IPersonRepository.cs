using Core.Entities;

namespace Core.Intefaces.Infrastructure.Repositories
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        Task DeleteAsync(Person person);
        Task UpdateAsync(Person person);
        Task<Person?> GetByIdAsync(Guid id);
        Task<IEnumerable<Person>> GetAllForUserAsync(Guid userId);
        Task<IEnumerable<Person>> GetUpcomingBirthdaysAsync(Guid userId, int startMonth, int startDay, int endMonth, int endDay);
    }
}