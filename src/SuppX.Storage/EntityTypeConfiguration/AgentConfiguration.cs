using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;

namespace SuppX.Storage.EntityTypeConfiguration;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder
        .HasKey(x => x.UserId);

        builder
        .HasOne(x => x.User)
        .WithOne()
        .HasForeignKey<Agent>(x => x.UserId);
    }
}
