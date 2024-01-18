using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Models;
using Money_Tracker.BLL.Services;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Repositories;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Money_Tracker",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")  // Permet les requêtes depuis l'origine spécifiée
                   .AllowAnyHeader()                      // Autorise tous les en-têtes dans les requêtes
                   .AllowAnyMethod();                     // Autorise toutes les méthodes HTTP
        });
});

// Ajout des services au conteneur de dépendances

// Configuration de la connexion à la base de données
builder.Services.AddTransient<DbConnection>(service =>
{
    string connectionString = builder.Configuration.GetConnectionString("Default");
    return new SqlConnection(connectionString);
});

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

// Configuration JWT (JSON Web Token)
JwtOptions options = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
builder.Services.AddSingleton(options);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        // Configuration de la validation du jeton JWT
        byte[] sKey = Encoding.UTF8.GetBytes(options.SigningKey);

        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                                // Valider l'émetteur du jeton
            ValidateAudience = true,                              // Valider le public cible du jeton
            ValidateLifetime = true,                              // Valider la durée de vie du jeton
            ValidateIssuerSigningKey = true,                      // Valider la clé de signature
            ValidIssuer = options.Issuer,                         // Émetteur du jeton
            ValidAudience = options.Audience,                     // Public cible du jeton
            IssuerSigningKey = new SymmetricSecurityKey(sKey)     // Clé de signature
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Configuration Swagger/OpenAPI pour la documentation de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Définition d'une sécurité pour le jeton JWT
    c.AddSecurityDefinition("Auth Token", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Ajouter le JWT nécessaire à l'authentification"
    });

    // Ajout d'un "verrou" sur toutes les routes de l'API pour exiger le JWT
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Auth Token" }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configuration du pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utilisation de CORS
app.UseCors("Money_Tracker");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
