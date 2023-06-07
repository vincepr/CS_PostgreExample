using CS_PostgreExample.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_PostgreExample.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Driver> Drives { get; set; }
    }
}
