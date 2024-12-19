using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Web.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var orders = await _mediator.Send(new GetOrdersQuery(page, pageSize));

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));

        if (order is null)
        {
            return NotFound($"Order with id {id} is not found.");
        }
        
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderForCreationDto? order)
    {
        if (order is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateOrderCommand(order));

        return CreatedAtAction(nameof(Create), order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderForUpdateDto? order)
    {
        if (order is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateOrderCommand(order));

        if (!isEntityFound)
        {
            return NotFound($"Order with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteOrderCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Order with id {id} is not found.");
        }

        return NoContent();
    }
}
