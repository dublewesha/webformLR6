using Microsoft.EntityFrameworkCore;
using webformLR6.Models;

namespace webformLR6.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
