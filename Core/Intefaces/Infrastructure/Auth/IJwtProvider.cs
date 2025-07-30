using Core.Entities;

namespace Core.Intefaces.Infrastructure.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}