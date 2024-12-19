using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Web.Controllers;

[Route("api/orderComponents")]
[ApiController]
public class OrderComponentController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderComponentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orderComponents = await _mediator.Send(new GetOrderComponentsQuery());

        return Ok(orderComponents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var orderComponent = await _mediator.Send(new GetOrderComponentByIdQuery(id));

        if (orderComponent is null)
        {
            return NotFound($"OrderComponent with id {id} is not found.");
        }
        
        return Ok(orderComponent);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderComponentForCreationDto? orderComponent)
    {
        if (orderComponent is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateOrderComponentCommand(orderComponent));

        return CreatedAtAction(nameof(Create), orderComponent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderComponentForUpdateDto? orderComponent)
    {
        if (orderComponent is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateOrderComponentCommand(orderComponent));

        if (!isEntityFound)
        {
            return NotFound($"OrderComponent with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteOrderComponentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"OrderComponent with id {id} is not found.");
        }

        return NoContent();
    }
}
