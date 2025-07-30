using Core.DTOs.Output.User;
using Core.Entities;

namespace Core.Intefaces.Application.Mapping
{
    public interface IUserMapper
    {
        UserResponse MapToResponse(User user);
        IEnumerable<UserResponse> MapToResponse(IEnumerable<User> users);
    }
}
