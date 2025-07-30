using Core.DTOs.Input.Auth;
using Core.DTOs.Output.Auth;

namespace Core.Intefaces.Application.Service
{
    public interface IAuthService
    {
        Task Register(RegisterUserRequest request);
        Task<AuthResponse> Login(LoginUserRequest request);
    }
}