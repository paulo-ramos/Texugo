using Microsoft.EntityFrameworkCore;
using Texugo.Login.Core.Contexts.AccountContext.Entities;
using Texugo.Login.Infraestrutura.Contexts.AccountContext.Mappings;

namespace Texugo.Login.Infraestrutura.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
    }
}