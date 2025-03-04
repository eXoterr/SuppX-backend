namespace SuppX.App.Models;

public class NewTicketRequest
{
    public int ClientId { get; set; }
    public string Theme { get; set; }
    public string Description { get; set; }
}
