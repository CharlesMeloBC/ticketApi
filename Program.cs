using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ticketApi.Data;
using ticketApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configura��o do banco de dados
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=appBank.db"));

        // Obten��o da chave JWT do appsettings.json
        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

        // Configura��o da autentica��o com JWT
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        // Autoriza��o por perfis (Roles)
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
            options.AddPolicy("MicaDeveloperPolicy", policy => policy.RequireRole("mica-developer"));
        });

        // Configura��o dos controladores e JSON
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

        // Swagger para documenta��o da API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ITicketService, TicketService>();

        var app = builder.Build();

        // Configura��o para ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configura��o do roteamento e middleware
        app.UseRouting();

        // Ordem correta do middleware: primeiro Autentica��o, depois Autoriza��o
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapeamento dos controladores
        app.MapControllers();

        app.Run();
    }
}
