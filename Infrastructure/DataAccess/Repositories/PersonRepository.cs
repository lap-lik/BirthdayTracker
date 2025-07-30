using Core.Entities;
using Core.Intefaces.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BirthdayTrackerDbContext _dbContext;


        public PersonRepository(BirthdayTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Person person)
        {
            await _dbContext.Persons.AddAsync(person);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Person person)
        {
            _dbContext.Persons.Update(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Person person)
        {
            _dbContext.Persons.Remove(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllForUserAsync(Guid userId)
        {
            return await _dbContext.Persons
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Birthday)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetUpcomingBirthdaysAsync(Guid userId, int startMonth, int startDay, int endMonth, int endDay)
        {
            return await _dbContext.Persons
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Where(p =>
                    (p.Birthday.Month > startMonth ||
                     (p.Birthday.Month == startMonth && p.Birthday.Day >= startDay)) &&
                    (p.Birthday.Month < endMonth ||
                     (p.Birthday.Month == endMonth && p.Birthday.Day <= endDay)))
                .OrderBy(p => p.Birthday.Month)
                .ThenBy(p => p.Birthday.Day)
                .ToListAsync();
        }
    }
}