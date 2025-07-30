using Core.Exceptions;
using Core.Intefaces.Infrastructure.Auth;
using Infrastructure.Auth.Options;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordOptions _passwordOptions;

        public PasswordHasher(IOptions<PasswordOptions> passwordOptions)
        {
            _passwordOptions = passwordOptions.Value;
        }

        public string Generate(string? password)
        {
            ValidatePassword(password);

            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string? password, string? hashedPassword)
        {
            ValidatePassword(password);
            ValidatePasswordOnNullOrEmpty(hashedPassword);

            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }


        private void ValidatePassword(string? password)
        {
            ValidatePasswordOnNullOrEmpty(password);

            if (!Regex.IsMatch(password, _passwordOptions.PasswordPattern))
            { 
                throw new InvalidPasswordException(_passwordOptions.PasswordErrorMessage);
            }
        }

        private void ValidatePasswordOnNullOrEmpty(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
        }
    }
}