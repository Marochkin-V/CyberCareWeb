using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Application.Requests.Commands;
using CyberCareWeb.Web.Controllers;

namespace CyberCareWeb.Tests.ControllersTests;

public class ComponentControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ComponentController _controller;

    public ComponentControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ComponentController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfComponents()
    {
        // Arrange
        var components = new List<ComponentDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetComponentsQuery(), CancellationToken.None))
            .ReturnsAsync(components);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<ComponentDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(components);

        _mediatorMock.Verify(m => m.Send(new GetComponentsQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingComponentId_ReturnsComponent()
    {
        // Arrange
        var componentId = Guid.NewGuid();
        var component = new ComponentDto { Id = componentId };

        _mediatorMock
            .Setup(m => m.Send(new GetComponentByIdQuery(componentId), CancellationToken.None))
            .ReturnsAsync(component);

        // Act
        var result = await _controller.GetById(componentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ComponentDto).Should().BeEquivalentTo(component);

        _mediatorMock.Verify(m => m.Send(new GetComponentByIdQuery(componentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingComponentId_ReturnsNotFoundResult()
    {
        // Arrange
        var componentId = Guid.NewGuid();
        var component = new ComponentDto { Id = componentId };

        _mediatorMock
            .Setup(m => m.Send(new GetComponentByIdQuery(componentId), CancellationToken.None))
            .ReturnsAsync((ComponentDto?)null);

        // Act
        var result = await _controller.GetById(componentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetComponentByIdQuery(componentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Component_ReturnsComponent()
    {
        // Arrange
        var component = new ComponentForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateComponentCommand(component), CancellationToken.None));

        // Act
        var result = await _controller.Create(component);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ComponentForCreationDto).Should().BeEquivalentTo(component);

        _mediatorMock.Verify(m => m.Send(new CreateComponentCommand(component), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_NullValue_ReturnsBadRequest()
    {
        // Arrange and Act
        var result = await _controller.Create(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new CreateComponentCommand(It.IsAny<ComponentForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingComponent_ReturnsNoContentResult()
    {
        // Arrange
        var componentId = Guid.NewGuid();
        var component = new ComponentForUpdateDto { Id = componentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateComponentCommand(component), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(componentId, component);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateComponentCommand(component), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingComponent_ReturnsNotFoundResult()
    {
        // Arrange
        var componentId = Guid.NewGuid();
        var component = new ComponentForUpdateDto { Id = componentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateComponentCommand(component), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(componentId, component);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateComponentCommand(component), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var componentId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(componentId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateComponentCommand(It.IsAny<ComponentForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingComponentId_ReturnsNoContentResult()
    {
        // Arrange
        var componentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteComponentCommand(componentId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(componentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteComponentCommand(componentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingComponentId_ReturnsNotFoundResult()
    {
        // Arrange
        var componentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteComponentCommand(componentId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(componentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteComponentCommand(componentId), CancellationToken.None), Times.Once);
    }
}

