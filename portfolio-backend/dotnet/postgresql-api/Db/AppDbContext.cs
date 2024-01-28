namespace postgresql_api.Db;
using Microsoft.EntityFrameworkCore;
using postgresql_api.Models;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<Student> Students { get; set; } = null!;
}
