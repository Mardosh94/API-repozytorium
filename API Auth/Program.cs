using API_Auth;
using API_Auth.Features.Employees.Services.EmployeeServices;
using API_Auth.Features.Employees.Services.TimesheetServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args); // Inicjalizacja aplikacji ASP.NET Core

// Dodanie us�ug do kontenera Dependency Injection (DI)


// Dodaje wsparcie dla kontroler�w API
builder.Services.AddControllers();

// Konfiguracja Swaggera/OpenAPI, umo�liwia generowanie dokumentacji API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
// Tworzy dokumentacj� Swagger z tytu�em i wersj� API
options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

// Dodaje definicj� zabezpiecze� dla token�w JWT
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
Name = "Authorization", // Nazwa nag��wka HTTP
Type = SecuritySchemeType.Http, // Typ zabezpiecze�: HTTP
Scheme = "bearer", // Schemat zabezpiecze�: Bearer Token
BearerFormat = "JWT", // Format tokena
In = ParameterLocation.Header, // Token znajduje si� w nag��wku HTTP
Description = "Input your Bearer token in the format: Bearer {your token}" // Opis dla u�ytkownika
});

// Wymaga, aby wszystkie operacje w Swaggerze u�ywa�y tego schematu zabezpiecze�
options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, // Odwo�anie do definicji zabezpiecze�
                    Id = "Bearer" // Identyfikator schematu "Bearer"
                }
            },
            Array.Empty<string>() // Brak dodatkowych wymaga�
        }
    });
});

// Konfiguracja uwierzytelniania (Bearer Token) i autoryzacji
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

// Konfiguracja bazy danych SQLite i modelu to�samo�ci
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db")); // Baza danych SQLite

builder.Services.AddIdentityCore<MyUser>(options =>
{
// Konfiguracja u�ytkownik�w
options.User.RequireUniqueEmail = true; // Wymaga unikalnych adres�w e-mail
options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+"; // Dozwolone znaki w nazwach u�ytkownik�w

// Konfiguracja hase�
options.Password.RequireDigit = false; // Nie wymaga cyfr w ha�le
options.Password.RequireNonAlphanumeric = false; // Nie wymaga znak�w specjalnych w ha�le
options.Password.RequireUppercase = false; // Nie wymaga wielkich liter w ha�le
options.Password.RequireLowercase = false; // Nie wymaga ma�ych liter w ha�le
})
    .AddEntityFrameworkStores<AppDbContext>() // U�ywa AppDbContext jako magazynu danych
    .AddApiEndpoints(); // Dodaje domy�lne endpointy API dla to�samo�ci

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

builder.Services.AddScoped<IEmployeeService, EmployeeService>();//////////////////////////////////Wa�ne zapami�taj!!!!!!!!!!!!
builder.Services.AddScoped<ITimesheetService, TimesheetService>();//////////////////////////////////Wa�ne zapami�taj!!!!!!!!!!!!
var app = builder.Build(); // Tworzy aplikacj� na podstawie skonfigurowanego buildera

// Konfiguracja potoku HTTP (Middleware)

// Je�li aplikacja dzia�a w trybie deweloperskim
if (app.Environment.IsDevelopment())
{
app.UseSwagger(); // W��cza generowanie dokumentacji Swagger
app.UseSwaggerUI(); // W��cza interfejs Swagger UI
}

//dodawanie do aplikacji CORS

app.UseCors("AllowReactApp"); // W��cz CORS dla aplikacji




app.UseHttpsRedirection(); // Przekierowuje ��dania HTTP na HTTPS

app.UseAuthorization(); // W��cza middleware autoryzacji

app.MapControllers(); // Mapuje kontrolery na odpowiednie trasy

app.Run(); // Uruchamia aplikacj�

// Definicja klasy u�ytkownika dziedzicz�cej po IdentityUser
public class MyUser : IdentityUser { }