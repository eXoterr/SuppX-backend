using Microsoft.EntityFrameworkCore;
using SuppX.Domain;
using SuppX.Storage.EntityTypeConfiguration;

namespace SuppX.Storage;

public class AppContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<AgentGroup> AgentGroups { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new AgentGroupConfiguration());
        modelBuilder.ApplyConfiguration(new AgentConfiguration());
    }
}
