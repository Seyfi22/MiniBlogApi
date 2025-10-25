using Microsoft.EntityFrameworkCore;
using MiniBlogApi.Model.Entities;

namespace MiniBlogApi.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        }

    }
}