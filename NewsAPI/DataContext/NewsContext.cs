using Microsoft.EntityFrameworkCore;
using NewsAPI.DataContext.Base;
using NewsAPI.Models;

namespace NewsAPI.DataContext;

public class NewsContext : DbContext, INewsContext
{
    public virtual DbSet<Article> Article { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Comment> Comment { get; set; }

    public NewsContext(DbContextOptions options) : base(options) { }
}
