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

public class OrderComponentControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrderComponentController _controller;

    public OrderComponentControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OrderComponentController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfOrderComponents()
    {
        // Arrange
        var orderComponents = new List<OrderComponentDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetOrderComponentsQuery(), CancellationToken.None))
            .ReturnsAsync(orderComponents);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<OrderComponentDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(orderComponents);

        _mediatorMock.Verify(m => m.Send(new GetOrderComponentsQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingOrderComponentId_ReturnsOrderComponent()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();
        var orderComponent = new OrderComponentDto { Id = orderComponentId };

        _mediatorMock
            .Setup(m => m.Send(new GetOrderComponentByIdQuery(orderComponentId), CancellationToken.None))
            .ReturnsAsync(orderComponent);

        // Act
        var result = await _controller.GetById(orderComponentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as OrderComponentDto).Should().BeEquivalentTo(orderComponent);

        _mediatorMock.Verify(m => m.Send(new GetOrderComponentByIdQuery(orderComponentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingOrderComponentId_ReturnsNotFoundResult()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();
        var orderComponent = new OrderComponentDto { Id = orderComponentId };

        _mediatorMock
            .Setup(m => m.Send(new GetOrderComponentByIdQuery(orderComponentId), CancellationToken.None))
            .ReturnsAsync((OrderComponentDto?)null);

        // Act
        var result = await _controller.GetById(orderComponentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetOrderComponentByIdQuery(orderComponentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_OrderComponent_ReturnsOrderComponent()
    {
        // Arrange
        var orderComponent = new OrderComponentForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateOrderComponentCommand(orderComponent), CancellationToken.None));

        // Act
        var result = await _controller.Create(orderComponent);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as OrderComponentForCreationDto).Should().BeEquivalentTo(orderComponent);

        _mediatorMock.Verify(m => m.Send(new CreateOrderComponentCommand(orderComponent), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateOrderComponentCommand(It.IsAny<OrderComponentForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingOrderComponent_ReturnsNoContentResult()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();
        var orderComponent = new OrderComponentForUpdateDto { Id = orderComponentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOrderComponentCommand(orderComponent), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(orderComponentId, orderComponent);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateOrderComponentCommand(orderComponent), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingOrderComponent_ReturnsNotFoundResult()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();
        var orderComponent = new OrderComponentForUpdateDto { Id = orderComponentId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOrderComponentCommand(orderComponent), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(orderComponentId, orderComponent);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateOrderComponentCommand(orderComponent), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(orderComponentId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateOrderComponentCommand(It.IsAny<OrderComponentForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingOrderComponentId_ReturnsNoContentResult()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOrderComponentCommand(orderComponentId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(orderComponentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteOrderComponentCommand(orderComponentId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingOrderComponentId_ReturnsNotFoundResult()
    {
        // Arrange
        var orderComponentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOrderComponentCommand(orderComponentId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(orderComponentId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteOrderComponentCommand(orderComponentId), CancellationToken.None), Times.Once);
    }
}

