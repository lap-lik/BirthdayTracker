using Core.DTOs.Output.User;
using Core.Entities;
using Core.Intefaces.Application.Mapping;

namespace Application.Mapping
{
    public class UserMapper : IUserMapper
    {
        public UserResponse MapToResponse(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new UserResponse(
                Id: user.Id,
                UserName: user.UserName,
                Email: user.Email,
                AvatarUrl: user.AvatarUrl
            );
        }

        public IEnumerable<UserResponse> MapToResponse(IEnumerable<User> users)
        {
            return users.Select(MapToResponse);
        }
    }
}