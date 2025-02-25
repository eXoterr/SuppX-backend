using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;
using SuppX.Utils;

namespace SuppX.Storage.EntityTypeConfiguration
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.HasData(
                new Role{ Id = Globals.ROLE_ADMIN_ID, Name = "admin"},
                new Role{ Id = Globals.ROLE_USER_ID, Name = "user"}
            );
        }
    }
}
