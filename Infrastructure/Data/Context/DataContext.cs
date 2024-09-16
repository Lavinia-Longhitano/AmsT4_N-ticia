using AmsT4_BLOG_2.Models;
using Microsoft.EntityFrameworkCore;

namespace AmsT4_BLOG_2.Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base (opts)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<News> Category { get; set; }
        public DbSet<News> Author { get; set; }
        public DbSet<News> Description { get; set; }
        public DbSet<News> Title { get; set; }
        public DbSet<News> PublishedDate { get; set; }
        public DbSet<News> ImageUrl { get; set; }
    }
}
