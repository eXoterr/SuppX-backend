using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;

namespace SuppX.Storage.EntityTypeConfiguration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);

        builder
        .HasOne(x => x.Agent)
        .WithMany(x => x.Tickets)
        .HasForeignKey(x => x.AgentId);

        builder
        .HasOne(x => x.Client)
        .WithMany()
        .HasForeignKey(x => x.ClientId);

        builder
        .HasOne(x => x.CloseReason)
        .WithMany()
        .HasForeignKey(x => x.CloseReasonId);

        builder
        .HasOne(x => x.Category)
        .WithMany()
        .HasForeignKey(x => x.CategoryId);
    }
}
