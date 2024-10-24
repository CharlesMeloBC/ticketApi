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

        // Configuração do banco de dados
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=appBank.db"));

        // Obtenção da chave JWT do appsettings.json
        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

        // Configuração da autenticação com JWT
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

        // Autorização por perfis (Roles)
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
            options.AddPolicy("MicaDeveloperPolicy", policy => policy.RequireRole("mica-developer"));
        });

        // Configuração dos controladores e JSON
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

        // Swagger para documentação da API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ITicketService, TicketService>();

        var app = builder.Build();

        // Configuração para ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configuração do roteamento e middleware
        app.UseRouting();

        // Ordem correta do middleware: primeiro Autenticação, depois Autorização
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapeamento dos controladores
        app.MapControllers();

        app.Run();
    }
}
