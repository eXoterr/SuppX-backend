using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;

namespace SuppX.Storage.EntityTypeConfiguration;

public class TokenBlacklistConfiguration : IEntityTypeConfiguration<BlacklistedToken>
{
    public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
