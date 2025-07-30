namespace Infrastructure.Auth.Options
{
    public class PasswordOptions
    {
        public string PasswordPattern { get; set; } = string.Empty;
        public string PasswordErrorMessage { get; set; } = string.Empty;
    }
}