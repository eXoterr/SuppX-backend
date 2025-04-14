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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <response code="201">Ticket created</response>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TicketModel newTicket, CancellationToken cancellationToken = default)
    {
        if(newTicket.Theme is null)
        {
            throw new BadRequestException("ticket theme cannot be null");
        }

        if(newTicket.Description is null)
        {
            throw new BadRequestException("ticket description cannot be null");
        }

        var ticket = new Ticket
        {
            ClientId = newTicket.ClientId,
            Theme = newTicket.Theme,
            Description = newTicket.Description
        };

        try
        {
            await ticketService.CreateAsync(ticket, cancellationToken);
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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [Authorize]
    [Route("closed")]
    [HttpPost]
    public async Task<IActionResult> CloseAsync([FromBody] CloseTicketRequest closeTicket, CancellationToken cancellationToken = default)
    {
        try
        {
            await ticketService.CloseAsync(closeTicket.TicketId, closeTicket.ReasonId, cancellationToken);
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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTicketsAsync(int offset, int limit = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            List<Ticket> tickets = await ticketService.GetAsync(offset, limit, cancellationToken);
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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] TicketUpdateModel newTicket, CancellationToken cancellationToken = default)
    {
        if (newTicket.Theme is null)
        {
            throw new BadRequestException("ticket theme cannot be null");
        }

        if (newTicket.Description is null)
        {
            throw new BadRequestException("ticket description cannot be null");
        }

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
            await ticketService.UpdateAsync(ticketDTO, cancellationToken);
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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [Authorize]
    [Route("reasons")]
    [HttpGet]
    public async Task<IActionResult> GetCloseReasonsAsync(int limit = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await ticketService.GetCloseReasonsAsync(limit, cancellationToken));
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
