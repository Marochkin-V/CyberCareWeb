using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Web.Controllers;

[Route("api/components")]
[ApiController]
public class ComponentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComponentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var components = await _mediator.Send(new GetComponentsQuery());

        return Ok(components);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var component = await _mediator.Send(new GetComponentByIdQuery(id));

        if (component is null)
        {
            return NotFound($"Component with id {id} is not found.");
        }
        
        return Ok(component);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ComponentForCreationDto? component)
    {
        if (component is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateComponentCommand(component));

        return CreatedAtAction(nameof(Create), component);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ComponentForUpdateDto? component)
    {
        if (component is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateComponentCommand(component));

        if (!isEntityFound)
        {
            return NotFound($"Component with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteComponentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Component with id {id} is not found.");
        }

        return NoContent();
    }
}
