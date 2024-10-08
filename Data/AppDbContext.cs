using Microsoft.EntityFrameworkCore;
using ticketApi.Models;

namespace ticketApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<TiketModel> Tikets { get; set; }
    }
}
