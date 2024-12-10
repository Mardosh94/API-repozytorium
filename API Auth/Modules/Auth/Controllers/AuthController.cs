using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API_Auth.Modules.Auth.Models;

namespace API_Auth.Modules.Auth.Controllers
{
    // Kontroler obsługujący operacje autoryzacji i uwierzytelniania
    [ApiController] // Atrybut oznaczający kontroler API
    [Route("[controller]")] // Definiuje ścieżkę bazową kontrolera (np. "/Auth")
    public class AuthController : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager; // Zarządza użytkownikami
        private readonly SignInManager<MyUser> _signInManager; // Obsługuje proces logowania

        // Konstruktor kontrolera, wstrzykuje zależności UserManager i SignInManager
        public AuthController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Endpoint rejestracji użytkownika
        [HttpPost("api/register")] // Ścieżka: /Auth/api/register
        public async Task<IResult> Register([FromBody] RegisterViewModel model)
        {
            // Walidacja danych wejściowych
            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Email))
            {
                return Results.BadRequest("Username, email, and password are required."); // Błąd: brak wymaganych pól
            }

            // Tworzy obiekt użytkownika na podstawie modelu
            var user = new MyUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            // Próbuje zarejestrować użytkownika z podanym hasłem
            var result = await _userManager.CreateAsync(user, model.Password);

            // Sprawdza, czy rejestracja się powiodła
            if (result.Succeeded)
            {
                return Results.Ok("User registered successfully."); // Sukces
            }

            return Results.BadRequest(result.Errors); // Błąd: zwraca szczegóły błędów
        }

        // Endpoint logowania użytkownika
        [HttpPost("api/login")] // Ścieżka: /Auth/api/login
        public async Task<IResult> Login(
            [FromBody] LoginViewModel body, // Dane logowania przesyłane w ciele żądania
            [FromQuery] bool? useCookies, // Flaga: czy używać ciasteczek do autoryzacji
            [FromQuery] bool? useSessionCookies, // Flaga: czy używać sesyjnych ciasteczek
            [FromServices] IServiceProvider sp // Usługi wstrzyknięte dynamicznie
        )
        {
            // Pobiera SignInManager z usług
            var signInManager = sp.GetRequiredService<SignInManager<MyUser>>();

            // Konfiguruje sposób autoryzacji (Bearer Token lub Cookies)
            var useCookieScheme = useCookies == true || useSessionCookies == true;
            var isPersistent = useCookies == true && useSessionCookies != true; // Zapamiętanie sesji
            signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            // Pobiera UserManager z usług
            var userManager = sp.GetRequiredService<UserManager<MyUser>>();

            MyUser user = null;

            // Sprawdza, czy podano e-mail, czy nazwę użytkownika
            if (body.EmailOrUserName is not null && body.EmailOrUserName.Contains("@"))
            {
                user = await userManager.FindByEmailAsync(body.EmailOrUserName); // Jeśli to e-mail
            }
            else
            {
                user = await userManager.FindByNameAsync(body.EmailOrUserName); // Jeśli to nazwa użytkownika
            }

            Microsoft.AspNetCore.Identity.SignInResult result = new();

            // Jeśli znaleziono użytkownika, próbuje zalogować
            if (user is not null)
                result = await signInManager.PasswordSignInAsync(user, body.Password, isPersistent, lockoutOnFailure: true);

            // Jeśli logowanie nie powiodło się, zwraca błąd 401 (Unauthorized)
            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            // Jeśli logowanie się powiodło, SignInManager sam ustawia odpowiedź (np. ciasteczka lub tokeny)
            return TypedResults.Empty;
        }
    }
}
