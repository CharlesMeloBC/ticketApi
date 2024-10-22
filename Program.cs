using Microsoft.EntityFrameworkCore;
using ticketApi.Data;
using ticketApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Registra o AppDbContext com SQLite
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=appBank.db"));

        // Serviços para controladores e Swagger
        builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ITicketService, TicketService>();


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