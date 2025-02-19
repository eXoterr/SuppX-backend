using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;

namespace SuppX.Storage.EntityTypeConfiguration;

public class CloseReasonConfiguration : IEntityTypeConfiguration<CloseReason>
{
    public void Configure(EntityTypeBuilder<CloseReason> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
