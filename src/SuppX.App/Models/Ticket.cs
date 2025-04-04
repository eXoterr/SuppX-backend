using System.ComponentModel.DataAnnotations;

namespace SuppX.App.Models;

public class TicketModel
{
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    [MinLength(10)]
    public string? Theme { get; set; }

    [Required]
    [MinLength(10)]
    public string? Description { get; set; }
}

public class TicketUpdateModel : TicketModel
{
    public int? AgentId { get; set; }
    public int? CloseReasonId { get; set; }
    public int? CategoryId { get; set; }
}

