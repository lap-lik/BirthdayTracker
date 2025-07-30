using Core.DTOs.Output.User;

namespace Core.DTOs.Output.Auth
{
    public record AuthResponse(
        string Token, 
        UserResponse User);
}