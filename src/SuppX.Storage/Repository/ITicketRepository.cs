using SuppX.Domain;

namespace SuppX.Storage.Repository;

public interface ITicketRepository
{
    Task<List<Ticket>> GetTicketsAsync(int offset, int limit, CancellationToken cancellationToken = default);

    Task CreateAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<List<CloseReason>> GetCloseReasonsAsync(int limit, CancellationToken cancellationToken = default);
    Task<CloseReason?> GetCloseReasonByIdAsync(int id, CancellationToken cancellationToken = default);
}
