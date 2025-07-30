using Core.Entities;

namespace Core.Intefaces.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
        Task<bool> Exists(string email);
        Task UpdateAsync(User user);
        Task UpdatePasswordAsync(Guid userId, string passwordHash);
    }
}