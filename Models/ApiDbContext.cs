using Microsoft.EntityFrameworkCore;

namespace MariyaBackend.Models;

public class ApiDbContext : DbContext
{
    public ApiDbContext() { }
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
