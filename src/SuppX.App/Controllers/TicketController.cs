using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuppX.App.Models;
using SuppX.Domain;
using SuppX.Service;

namespace SuppX.App.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    [Authorize]
    [Route("new")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NewTicketRequest newTicket)
    {
        var ticket = new Ticket
        {
            ClientId = newTicket.ClientId,
            Theme = newTicket.Theme,
            Description = newTicket.Description
        };

        await ticketService.CreateAsync(ticket);

        return Created();
    }

    [Authorize]
    [Route("list")]
    [HttpGet]
    public async Task<IActionResult> GetTicketsAsync(int offset, int limit = 10)
    {
        List<Ticket> tickets = await ticketService.GetTicketsAsync(offset, limit);

        return Ok(tickets);
    }
}
