using Microsoft.EntityFrameworkCore;
using ticketApi.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Registra o AppDbContext com SQLite
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=appBank.db"));

        // Serviços para controladores e Swagger
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}