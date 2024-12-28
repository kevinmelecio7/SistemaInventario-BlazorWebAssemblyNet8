using AppInvWebSharedLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AppInvWebServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
