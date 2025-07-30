namespace Core.DTOs.Output.User
{
    public record UserResponse(
        Guid Id, 
        string UserName, 
        string Email, 
        string? AvatarUrl);
}
