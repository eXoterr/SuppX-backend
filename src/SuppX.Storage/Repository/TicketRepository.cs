using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using SuppX.Domain;

namespace SuppX.Storage.Repository;

public class TicketRepository(ApplicationContext context) : ITicketRepository
{
    public async Task<List<Ticket>> GetTicketsAsync(int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        return await context.Tickets
                            .OrderBy(x => x.CreatedAt)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        ticket.CreatedAt = DateTime.UtcNow;
        await context.Tickets.AddAsync(ticket, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Ticket? ticket = await context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return ticket;
    }
    public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        context.Tickets.Update(ticket);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CloseReason>> GetCloseReasonsAsync(int limit, CancellationToken cancellationToken = default)
    {
        return await context.CloseReasons
                            .OrderBy(x => x.Id)
                            .Take(limit)
                            .ToListAsync(cancellationToken);
    }

    public async Task<CloseReason?> GetCloseReasonByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.CloseReasons.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}
