using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;

namespace NewsAPI.DataContext.Base;

public interface INewsContext
{
    DbSet<Article> Article { get; set; }

    DbSet<User> User { get; set; }

    DbSet<Comment> Comment { get; set; }

    int SaveChanges();
}
