using System;

namespace SuppX.Domain;

public class Ticket
{
    public int Id { get; set; }
    public User? Client { get; set; }
    public required string Theme { get; set; }
    public required string Description { get; set; }
    public Agent? Agent { get; set; }
    public TicketCategory? Category { get; set; }
    public CloseReason? CloseReason { get; set; }
    public DateTime CreatedAt  { get; set; }
    public DateTime ClosedAt  { get; set; }

    public int? AgentId { get; set; }
    public int ClientId { get; set; }
    public int? CloseReasonId { get; set; }
    public int? CategoryId { get; set; }
}
