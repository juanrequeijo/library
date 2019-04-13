using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options)
            : base(options)
        {
        }

        public DbSet<SearchItem> SearchItems { get; set; }
    }
}