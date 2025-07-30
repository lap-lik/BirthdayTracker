namespace Core.Intefaces.Infrastructure.Auth
{
    public interface IEmailValidator
    {
        void IsValidEmail(string email);
    }
}