using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppX.Storage.EntityTypeConfiguration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .Property(x => x.RoleId)
            .HasDefaultValue(0);

            builder.HasKey(u => u.Id);
            builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

            builder.Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(u => u.Login)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
