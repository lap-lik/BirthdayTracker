using Core.DTOs.Input.User;
using Core.DTOs.Output.User;
using Core.Enums;
using Core.Exceptions;
using Core.Intefaces.Application.Mapping;
using Core.Intefaces.Infrastructure.Auth;
using Core.Intefaces.Infrastructure.Service;
using Core.Intefaces.Repositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMediaService _mediaService;
        private readonly IUserMapper _userMapper;
        private readonly IEmailValidator _emailValidator;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IMediaService mediaService,
            IUserMapper userMapper,
            IEmailValidator emailValidator
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mediaService = mediaService;
            _userMapper = userMapper;
            _emailValidator = emailValidator;
        }

        public async Task<UserResponse> UpdateAsync(Guid userId, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException($"User with ID {userId} not found");

            string? newPasswordHash = request.Password != null
                ? _passwordHasher.Generate(request.Password)
                : null;

            if (!string.IsNullOrEmpty(request.Email))
            {
                _emailValidator.IsValidEmail(request.Email);

                if (await _userRepository.Exists(request.Email))
                {
                    throw new ConflictException($"Email '{request.Email}' is already registered");
                }
            }

            string? avatarUrl = request.Avatar != null
                ? await _mediaService.UpdateImageAsync(user.AvatarUrl, request.Avatar, MediaType.UserAvatar)
                : null;

            user.Update(request.UserName, request.Email, avatarUrl, newPasswordHash);

            await _userRepository.UpdateAsync(user);

            return _userMapper.MapToResponse(user);
        }

        public async Task UpdatePasswordAsync(Guid userId, ChangePasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException($"User with ID {userId} not found");

            if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
            {
                throw new InvalidPasswordException("The current password is incorrect");
            }

            var newPasswordHash = _passwordHasher.Generate(request.NewPassword);

            await _userRepository.UpdatePasswordAsync(userId, newPasswordHash);

        }
    }
}