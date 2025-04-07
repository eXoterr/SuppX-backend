using Microsoft.EntityFrameworkCore;
using SuppX.Domain;
using SuppX.Storage.EntityTypeConfiguration;

namespace SuppX.Storage;

public class ApplicationContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<AgentGroup> AgentGroups { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<CloseReason> CloseReasons { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder contextOptions)
    // {
    //     base.OnConfiguring(contextOptions);
    //     contextOptions.UseNpgsql(config.GetConnectionString());
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new AgentGroupConfiguration());
        modelBuilder.ApplyConfiguration(new AgentConfiguration());
        modelBuilder.ApplyConfiguration(new TicketCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CloseReasonConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
    }
}
