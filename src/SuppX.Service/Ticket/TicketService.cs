using SuppX.Domain;
using SuppX.Storage;
using SuppX.Storage.Repository;

namespace SuppX.Service;

public class TicketService(ITicketRepository ticketRepository, IUserRepository userRepository) : ITicketService
{
    public async Task CreateAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        await ticketRepository.CreateAsync(ticket, cancellationToken);
    }

    public async Task<bool> ExistsAsync(int ticketId, CancellationToken cancellationToken = default)
    {
        Ticket? ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        return ticket is not null;
    }
    public async Task CloseAsync(int ticketId, int reasonId, CancellationToken cancellationToken = default)
    {
        Ticket? ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            // TODO: log error
            return;
        }
        ticket.ClosedAt = DateTime.UtcNow;
        ticket.CloseReasonId = reasonId;

        await ticketRepository.UpdateAsync(ticket, cancellationToken);
    }
    public async Task AssignCategoryAsync(int ticketId, int categoryId, CancellationToken cancellationToken = default)
    {
        Ticket? ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            // TODO: log error
            return;
        }

        ticket.CategoryId = categoryId;

        await ticketRepository.UpdateAsync(ticket, cancellationToken);
    }

    public async Task AssignAgentAsync(int ticketId, int agentId, CancellationToken cancellationToken = default)
    {
        Ticket? ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            // TODO: log error
            return;
        }
        User? agent = await userRepository.GetByIdAsync(agentId, cancellationToken);
        if (agent is null)
        {
            // TODO: log error
            return;
        }

        ticket.AgentId = agent.Id;

        await ticketRepository.UpdateAsync(ticket, cancellationToken);
    }

    public async Task<List<Ticket>> GetTicketsAsync(int offset, int limit, CancellationToken cancellationToken)
    {
        return await ticketRepository.GetTicketsAsync(offset, limit, cancellationToken);
    }

    public async Task<List<CloseReason>> GetCloseReasonsAsync(int limit, CancellationToken cancellationToken)
    {
        return await ticketRepository.GetCloseReasonsAsync(limit, cancellationToken);
    }
}
