using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuppX.App.Models;
using SuppX.Domain;
using SuppX.Service;

namespace SuppX.App.Controllers;

[ApiController]
[Route("tickets")]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    /// <summary>
    /// Creates new support request Ticket
    /// </summary>
    /// <param name="newTicket">Request, containing client id, theme and description of problem</param>
    /// <response code="201">Ticket created</response>
    [Authorize]
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

    /// <summary>
    /// Returns list of support Tickets
    /// </summary>
    /// <param name="offset">Offset from start, used for pagination</param>
    /// <param name="limit">Max amout of Tickets to return</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTicketsAsync(int offset, int limit = 10)
    {
        List<Ticket> tickets = await ticketService.GetTicketsAsync(offset, limit);

        return Ok(tickets);
    }

    /// <summary>
    /// Returns available ticket Close Reasons 
    /// </summary>
    /// <param name="limit">Limit of Close Reasons</param>
    /// <returns></returns>
    [Authorize]
    [Route("reasons")]
    [HttpGet]
    public async Task<IActionResult> GetCloseReasonsAsync(int limit = 10)
    {
        List<CloseReason> closeReasons = await ticketService.GetCloseReasonsAsync(limit);

        return Ok(closeReasons);
    }
}
