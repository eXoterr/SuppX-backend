using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;

namespace SuppX.Storage.EntityTypeConfiguration;

public class AgentGroupConfiguration : IEntityTypeConfiguration<AgentGroup>
{
    public void Configure(EntityTypeBuilder<AgentGroup> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
