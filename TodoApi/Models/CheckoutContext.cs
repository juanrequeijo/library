using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class CheckoutContext : DbContext
    {
        public CheckoutContext(DbContextOptions<CheckoutContext> options)
            : base(options)
        {
        }

        public DbSet<CheckoutItem> CheckoutItems { get; set; }
    }
}