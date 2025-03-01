using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuppX.Domain;

namespace SuppX.Storage;

public class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategory>
{
    public void Configure(EntityTypeBuilder<TicketCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasData(
            new TicketCategory
            {
                Id = 1,
                Name = "Спам"
            },
            new TicketCategory
            {
                Id = 2,
                Name = "Доставка"
            },
            new TicketCategory
            {
                Id = 3,
                Name = "Качество еды"
            },
            new TicketCategory
            {
                Id = 4,
                Name = "Сервис от курьера"
            },
            new TicketCategory
            {
                Id = 5,
                Name = "Технические неполадки"
            },
            new TicketCategory
            {
                Id = 6,
                Name = "Прочее"
            }
        );
    }
}
