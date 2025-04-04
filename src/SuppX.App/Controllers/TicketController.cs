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
    public async Task<IActionResult> CreateAsync([FromBody] TicketModel newTicket)
    {
        var ticket = new Ticket
        {
            ClientId = newTicket.ClientId,
            Theme = newTicket.Theme,
            Description = newTicket.Description
        };

        try
        {
            await ticketService.CreateAsync(ticket);
        }
        catch (BadRequestException error)
        {
            return BadRequest(new JSONError(error.Message));
        }
        catch (Exception error)
        {
            return Problem(error.Message);
        }

        return Created();
    }

    /// <summary>
    /// Closes opened Ticket with selected Reason
    /// </summary>
    /// <param name="closeTicket"></param>
    /// <returns></returns>
    [Authorize]
    [Route("closed")]
    [HttpPost]
    public async Task<IActionResult> CloseAsync([FromBody] CloseTicketRequest closeTicket)
    {
        try
        {
            await ticketService.CloseAsync(closeTicket.TicketId, closeTicket.ReasonId);
        }
        catch (BadRequestException error)
        {
            return BadRequest(new JSONError(error.Message));
        }
        catch (Exception error)
        {
            return Problem(error.Message);
        }

        return Ok();
    }

    /// <summary>
    /// Returns list of support Tickets
    /// </summary>
    /// <param name="offset">Offset from start, used for pagination</param>
    /// <param name="limit">Max amount of Tickets to return</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTicketsAsync(int offset, int limit = 10)
    {
        try
        {
            List<Ticket> tickets = await ticketService.GetAsync(offset, limit);
            return Ok(tickets);
        }
        catch (BadRequestException error)
        {
            return BadRequest(new JSONError(error.Message));
        }
        catch (Exception error)
        {
            return Problem(error.Message);
        }
    }

    /// <summary>
    ///  Updates existing Ticket's properties
    /// </summary>
    /// <param name="newTicket">Updated Ticket object</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] TicketUpdateModel newTicket)
    {
        var ticketDTO = new Ticket
        {
            Id = newTicket.Id,
            ClientId = newTicket.ClientId,
            Theme = newTicket.Theme,
            Description = newTicket.Description,
            AgentId = newTicket.AgentId,
            CategoryId = newTicket.CategoryId,
        };


        try
        {
            await ticketService.UpdateAsync(ticketDTO);
            return Ok();
        }
        catch (BadRequestException error)
        {
            return BadRequest(new JSONError(error.Message));
        }
        catch (Exception error)
        {
            return Problem(error.Message);
        }
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
        try
        {
            return Ok(await ticketService.GetCloseReasonsAsync(limit));
        }
        catch (BadRequestException error)
        {
            return BadRequest(new JSONError(error.Message));
        }
        catch (Exception error)
        {
            return Problem(error.Message);
        }
    }
}
