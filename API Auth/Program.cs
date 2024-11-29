using API_Auth;
using API_Auth.Features.Employees.Services.EmployeeServices;
using API_Auth.Features.Employees.Services.TimesheetServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args); // Inicjalizacja aplikacji ASP.NET Core

// Dodanie us³ug do kontenera Dependency Injection (DI)


// Dodaje wsparcie dla kontrolerów API
builder.Services.AddControllers();

// Konfiguracja Swaggera/OpenAPI, umo¿liwia generowanie dokumentacji API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
// Tworzy dokumentacjê Swagger z tytu³em i wersj¹ API
options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

// Dodaje definicjê zabezpieczeñ dla tokenów JWT
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
Name = "Authorization", // Nazwa nag³ówka HTTP
Type = SecuritySchemeType.Http, // Typ zabezpieczeñ: HTTP
Scheme = "bearer", // Schemat zabezpieczeñ: Bearer Token
BearerFormat = "JWT", // Format tokena
In = ParameterLocation.Header, // Token znajduje siê w nag³ówku HTTP
Description = "Input your Bearer token in the format: Bearer {your token}" // Opis dla u¿ytkownika
});

// Wymaga, aby wszystkie operacje w Swaggerze u¿ywa³y tego schematu zabezpieczeñ
options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, // Odwo³anie do definicji zabezpieczeñ
                    Id = "Bearer" // Identyfikator schematu "Bearer"
                }
            },
            Array.Empty<string>() // Brak dodatkowych wymagañ
        }
    });
});

// Konfiguracja uwierzytelniania (Bearer Token) i autoryzacji
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

// Konfiguracja bazy danych SQLite i modelu to¿samoœci
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db")); // Baza danych SQLite

builder.Services.AddIdentityCore<MyUser>(options =>
{
// Konfiguracja u¿ytkowników
options.User.RequireUniqueEmail = true; // Wymaga unikalnych adresów e-mail
options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+"; // Dozwolone znaki w nazwach u¿ytkowników

// Konfiguracja hase³
options.Password.RequireDigit = false; // Nie wymaga cyfr w haœle
options.Password.RequireNonAlphanumeric = false; // Nie wymaga znaków specjalnych w haœle
options.Password.RequireUppercase = false; // Nie wymaga wielkich liter w haœle
options.Password.RequireLowercase = false; // Nie wymaga ma³ych liter w haœle
})
    .AddEntityFrameworkStores<AppDbContext>() // U¿ywa AppDbContext jako magazynu danych
    .AddApiEndpoints(); // Dodaje domyœlne endpointy API dla to¿samoœci

//dodawanie adresu react
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Adres React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IEmployeeService, EmployeeService>();//////////////////////////////////Wa¿ne zapamiêtaj!!!!!!!!!!!!
builder.Services.AddScoped<ITimesheetService, TimesheetService>();//////////////////////////////////Wa¿ne zapamiêtaj!!!!!!!!!!!!
var app = builder.Build(); // Tworzy aplikacjê na podstawie skonfigurowanego buildera

// Konfiguracja potoku HTTP (Middleware)

// Jeœli aplikacja dzia³a w trybie deweloperskim
if (app.Environment.IsDevelopment())
{
app.UseSwagger(); // W³¹cza generowanie dokumentacji Swagger
app.UseSwaggerUI(); // W³¹cza interfejs Swagger UI
}

//dodawanie do aplikacji CORS

app.UseCors("AllowReactApp"); // W³¹cz CORS dla aplikacji




app.UseHttpsRedirection(); // Przekierowuje ¿¹dania HTTP na HTTPS

app.UseAuthorization(); // W³¹cza middleware autoryzacji

app.MapControllers(); // Mapuje kontrolery na odpowiednie trasy

app.Run(); // Uruchamia aplikacjê

// Definicja klasy u¿ytkownika dziedzicz¹cej po IdentityUser
public class MyUser : IdentityUser { }