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

        builder.HasData([
            new CloseReason
            {
                Id = 1,
                Name = "Проблема решена"
            },
            new CloseReason
            {
                Id = 2,
                Name = "Нет ответа"
            },
            new CloseReason
            {
                Id = 3,
                Name = "Некорректное общение"
            },
            new CloseReason
            {
                Id = 4,
                Name = "Мошенничество"
            },
        ]);
    }
}
