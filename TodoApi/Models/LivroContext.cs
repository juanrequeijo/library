using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class LivroContext : DbContext
    {
        public LivroContext(DbContextOptions<LivroContext> options)
            : base(options)
        {
        }

        public DbSet<LivroItem> LivroItems { get; set; }
    }
}