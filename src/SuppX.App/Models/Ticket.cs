using System.ComponentModel.DataAnnotations;

namespace SuppX.App.Models;

public class TicketModel
{
    [Required]
    public int ClientId { get; set; }

    [Required]
    [MinLength(10)]
    public string? Theme { get; set; }

    [Required]
    [MinLength(10)]
    public string? Description { get; set; }
}
