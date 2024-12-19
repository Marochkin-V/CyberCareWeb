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

public class ComponentTypeControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ComponentTypeController _controller;

    public ComponentTypeControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ComponentTypeController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfComponentTypes()
    {
        // Arrange
        var componentTypes = new List<ComponentTypeDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetComponentTypesQuery(), CancellationToken.None))
            .ReturnsAsync(componentTypes);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<ComponentTypeDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(componentTypes);

        _mediatorMock.Verify(m => m.Send(new GetComponentTypesQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingComponentTypeId_ReturnsComponentType()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();
        var componentType = new ComponentTypeDto { Id = componentTypeId };

        _mediatorMock
            .Setup(m => m.Send(new GetComponentTypeByIdQuery(componentTypeId), CancellationToken.None))
            .ReturnsAsync(componentType);

        // Act
        var result = await _controller.GetById(componentTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ComponentTypeDto).Should().BeEquivalentTo(componentType);

        _mediatorMock.Verify(m => m.Send(new GetComponentTypeByIdQuery(componentTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingComponentTypeId_ReturnsNotFoundResult()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();
        var componentType = new ComponentTypeDto { Id = componentTypeId };

        _mediatorMock
            .Setup(m => m.Send(new GetComponentTypeByIdQuery(componentTypeId), CancellationToken.None))
            .ReturnsAsync((ComponentTypeDto?)null);

        // Act
        var result = await _controller.GetById(componentTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetComponentTypeByIdQuery(componentTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_ComponentType_ReturnsComponentType()
    {
        // Arrange
        var componentType = new ComponentTypeForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateComponentTypeCommand(componentType), CancellationToken.None));

        // Act
        var result = await _controller.Create(componentType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ComponentTypeForCreationDto).Should().BeEquivalentTo(componentType);

        _mediatorMock.Verify(m => m.Send(new CreateComponentTypeCommand(componentType), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateComponentTypeCommand(It.IsAny<ComponentTypeForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingComponentType_ReturnsNoContentResult()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();
        var componentType = new ComponentTypeForUpdateDto { Id = componentTypeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateComponentTypeCommand(componentType), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(componentTypeId, componentType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateComponentTypeCommand(componentType), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingComponentType_ReturnsNotFoundResult()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();
        var componentType = new ComponentTypeForUpdateDto { Id = componentTypeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateComponentTypeCommand(componentType), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(componentTypeId, componentType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateComponentTypeCommand(componentType), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(componentTypeId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateComponentTypeCommand(It.IsAny<ComponentTypeForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingComponentTypeId_ReturnsNoContentResult()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteComponentTypeCommand(componentTypeId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(componentTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteComponentTypeCommand(componentTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingComponentTypeId_ReturnsNotFoundResult()
    {
        // Arrange
        var componentTypeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteComponentTypeCommand(componentTypeId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(componentTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteComponentTypeCommand(componentTypeId), CancellationToken.None), Times.Once);
    }
}

