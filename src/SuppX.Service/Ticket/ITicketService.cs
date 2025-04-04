using SuppX.Domain;

namespace SuppX.Service;

public interface ITicketService
{
    Task CreateAsync(Ticket ticket, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int ticketId, CancellationToken cancellationToken = default);
    Task CloseAsync(int ticketId, int reasonId, CancellationToken cancellationToken = default);
    Task AssignCategoryAsync(int ticketId, int categoryId, CancellationToken cancellationToken = default);
    Task AssignAgentAsync(int ticketId, int categoryId, CancellationToken cancellationToken = default);
    Task<List<Ticket>> GetAsync(int offset, int limit, CancellationToken cancellationToken = default);
    Task<List<CloseReason>> GetCloseReasonsAsync(int limit, CancellationToken cancellationToken = default);
    Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default);

}
