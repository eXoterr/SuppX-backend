using Microsoft.AspNetCore.Mvc;
using SuppX.Domain;
using SuppX.Service;

namespace SuppX.App.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    [Route("new")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(int clientId, string theme, string description)
    {
        var ticket = new Ticket
        {
            ClientId = clientId,
            Theme = theme,
            Description = description
        };

        await ticketService.CreateAsync(ticket);

        return Created();
    }
}
