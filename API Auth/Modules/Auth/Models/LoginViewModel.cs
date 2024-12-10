namespace API_Auth.Modules.Auth.Models
{
    public sealed class LoginViewModel
    {
        public required string EmailOrUserName { get; init; } // E-mail lub nazwa użytkownika
        public required string Password { get; init; } // Hasło
    }
}
