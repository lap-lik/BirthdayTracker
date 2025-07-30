using Core.DTOs.Input.User;
using Core.DTOs.Output.User;

public interface IUserService
{
    Task<UserResponse> UpdateAsync(Guid userId, UpdateUserRequest request);
    Task UpdatePasswordAsync(Guid userId, ChangePasswordRequest request);
}