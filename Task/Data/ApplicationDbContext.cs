using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Personnel_Records> Personnel_Records { get; set; }
    }
}
