using System.ComponentModel.DataAnnotations;

namespace SuppX.App.Models;

public class CloseTicketRequest
{
    [Required]
    public int TicketId { get; set; }
    [Required]
    public int ReasonId { get; set; }
}
