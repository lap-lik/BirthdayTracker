namespace Infrastructure.Auth.Options
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpitestHours { get; set; }
    }
}