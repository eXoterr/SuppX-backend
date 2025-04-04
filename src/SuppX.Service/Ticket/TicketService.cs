using SuppX.Domain;
using SuppX.Storage;
using SuppX.Storage.Repository;

namespace SuppX.Service;

public class TicketService(ITicketRepository ticketRepository, IUserRepository userRepository) : ITicketService
{
    public async Task CreateAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        var isUserExists = await userRepository.GetByIdAsync(ticket.ClientId) is not null;
        if (!isUserExists)
        {
            throw new BadRequestException("specified ClientId does not exists");
        }

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
            throw new BadRequestException("Specified TicketId is not found");
        }
        if (ticket.ClosedAt != DateTime.MinValue)
        {
            throw new BadRequestException("This ticket is already closed");
        }

        CloseReason? reason = await ticketRepository.GetCloseReasonByIdAsync(reasonId);
        if (reason is null)
        {
            throw new BadRequestException("Specified CloseReasonId is not found");
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
            throw new BadRequestException("TicketId not found");      
        }
        User? agent = await userRepository.GetByIdAsync(agentId, cancellationToken);
        if (agent is null)
        {
            throw new BadRequestException("AgentId not found");      
        }

        ticket.AgentId = agent.Id;

        await ticketRepository.UpdateAsync(ticket, cancellationToken);
    }

    public async Task<List<Ticket>> GetAsync(int offset, int limit, CancellationToken cancellationToken)
    {
        if (limit < 0)
        {
            throw new BadRequestException("Limit can't be negative");
        }
        if (offset < 0)
        {
            throw new BadRequestException("Offset can't be negative");
        }
        return await ticketRepository.GetTicketsAsync(offset, limit, cancellationToken);
    }

    public async Task<List<CloseReason>> GetCloseReasonsAsync(int limit, CancellationToken cancellationToken)
    {
        if (limit < 0)
        {
            throw new BadRequestException("Limit can't be negative");
        }
        return await ticketRepository.GetCloseReasonsAsync(limit, cancellationToken);
    }

    public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        Ticket? storedTicket = await ticketRepository.GetByIdAsync(ticket.Id, cancellationToken);
        if (storedTicket is null)
        {
            throw new BadRequestException("TicketId not found");
        }
        await ticketRepository.UpdateAsync(ticket, cancellationToken);
    }

}
