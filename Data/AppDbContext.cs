using Microsoft.EntityFrameworkCore;
using ticketApi.Models;

namespace ticketApi.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet <TiketModel> Tikets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>
        
          optionsBuilder.UseSqlite(connectionString:"Data Source=appBank.db");      

    }
}
