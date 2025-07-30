using Core.Entities;
using Core.Intefaces.Repositories;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BirthdayTrackerDbContext _dbContext;

        public UserRepository(BirthdayTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Exists(string email)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        public async Task UpdatePasswordAsync(Guid userId, string newPasswordHash)
        {
            await _dbContext.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(u => u.PasswordHash, newPasswordHash));
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}