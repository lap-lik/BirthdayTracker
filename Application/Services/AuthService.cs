using Core.DTOs.Input.Auth;
using Core.DTOs.Output.Auth;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Intefaces.Application.Mapping;
using Core.Intefaces.Application.Service;
using Core.Intefaces.Infrastructure.Auth;
using Core.Intefaces.Infrastructure.Service;
using Core.Intefaces.Repositories;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMediaService _mediaService;
        private readonly IUserMapper _userMapper;
        private readonly IEmailValidator _emailValidator;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IMediaService mediaService,
            IUserMapper userMapper,
            IEmailValidator emailValidator
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _mediaService = mediaService;
            _userMapper = userMapper;
            _emailValidator = emailValidator;
        }

        public async Task Register(RegisterUserRequest request)
        {
            var hashedPassword = _passwordHasher.Generate(request.Password);

            _emailValidator.IsValidEmail(request.Email);

            if (await _userRepository.Exists(request.Email))
            {
                throw new ConflictException($"Email '{request.Email}' is already registered");
            }

            string? avatarUrl = request.Avatar != null
                ? await _mediaService.SaveImageAsync(request.Avatar, MediaType.UserAvatar)
                : null;

            var user = User.Create(Guid.NewGuid(), request.UserName, request.Email, hashedPassword, avatarUrl);

            await _userRepository.Add(user);
        }

        public async Task<AuthResponse> Login(LoginUserRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new NotFoundException($"User with Email '{request.Email}' not found"); ;

            var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new InvalidPasswordException("Invalid password");
            }

            var token = _jwtProvider.GenerateToken(user);
            var userResponse = _userMapper.MapToResponse(user);

            return new AuthResponse(token, userResponse);
        }
    }
}