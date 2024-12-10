namespace API_Auth.Modules.Auth.Models
{
    public class RegisterViewModel
    {
        public required string UserName { get; set; } // Nazwa użytkownika
        public required string Email { get; set; } // Adres e-mail
        public required string Password { get; set; } // Hasło
        public required string ConfirmPassword { get; set; } // Potwierdzenie hasła
    }
}
