using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Web.Controllers;

[Route("api/componentTypes")]
[ApiController]
public class ComponentTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComponentTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var componentTypes = await _mediator.Send(new GetComponentTypesQuery(page, pageSize));

        return Ok(componentTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var componentType = await _mediator.Send(new GetComponentTypeByIdQuery(id));

        if (componentType is null)
        {
            return NotFound($"ComponentType with id {id} is not found.");
        }
        
        return Ok(componentType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ComponentTypeForCreationDto? componentType)
    {
        if (componentType is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateComponentTypeCommand(componentType));

        return CreatedAtAction(nameof(Create), componentType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ComponentTypeForUpdateDto? componentType)
    {
        if (componentType is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateComponentTypeCommand(componentType));

        if (!isEntityFound)
        {
            return NotFound($"ComponentType with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteComponentTypeCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ComponentType with id {id} is not found.");
        }

        return NoContent();
    }
}
