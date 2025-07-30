using Core.Exceptions;
using Core.Intefaces.Infrastructure.Auth;
using Infrastructure.Auth.Options;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Infrastructure.Auth
{
    public class EmailValidator : IEmailValidator
    {
        private readonly EmailOptions _emailOptions;

        public EmailValidator(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public void IsValidEmail(string email)
        {
            if (!Regex.IsMatch(email, _emailOptions.EmailPattern) || string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException(_emailOptions.EmailErrorMessage);
            }
        }
    }
}