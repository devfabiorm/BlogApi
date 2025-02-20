namespace BlogApi;

public static class Configuration
{
    public static string JwtKey = "QDjMNFhqaz8Ld35GVNb0ygNmHQLhD0Ipsuj7";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_4%<i5-d3M1c£Xy]5";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 25;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
