namespace Infrastructure.Auth.Options
{
    public class EmailOptions
    {
        public string EmailPattern { get; set; } = string.Empty;
        public string EmailErrorMessage { get; set; } = string.Empty;
    }
}