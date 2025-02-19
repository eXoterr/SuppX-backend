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
    class UserProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
            .HasKey(x => x.UserId);

            builder
            .HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<Profile>(x => x.UserId);
        }
    }
}
