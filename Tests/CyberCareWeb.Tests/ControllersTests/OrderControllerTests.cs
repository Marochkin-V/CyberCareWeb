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

public class OrderControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OrderController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfOrders()
    {
        // Arrange
        var orders = new List<OrderDto> { new(), new() };
        var pagedResult = new PagedResult<OrderDto>(orders, orders.Count, 1, 10);

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetOrdersQuery>(q => q.Page == 1 && q.PageSize == 10), CancellationToken.None))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as PagedResult<OrderDto>;
        value.Should().NotBeNull();
        value!.Items.Should().HaveCount(2);
        value.Items.Should().BeEquivalentTo(orders);

        _mediatorMock.Verify(m => m.Send(It.Is<GetOrdersQuery>(q => q.Page == 1 && q.PageSize == 10), CancellationToken.None), Times.Once);
    }


    [Fact]
    public async Task GetById_ExistingOrderId_ReturnsOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new OrderDto { Id = orderId };

        _mediatorMock
            .Setup(m => m.Send(new GetOrderByIdQuery(orderId), CancellationToken.None))
            .ReturnsAsync(order);

        // Act
        var result = await _controller.GetById(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as OrderDto).Should().BeEquivalentTo(order);

        _mediatorMock.Verify(m => m.Send(new GetOrderByIdQuery(orderId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingOrderId_ReturnsNotFoundResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new OrderDto { Id = orderId };

        _mediatorMock
            .Setup(m => m.Send(new GetOrderByIdQuery(orderId), CancellationToken.None))
            .ReturnsAsync((OrderDto?)null);

        // Act
        var result = await _controller.GetById(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetOrderByIdQuery(orderId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Order_ReturnsOrder()
    {
        // Arrange
        var order = new OrderForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateOrderCommand(order), CancellationToken.None));

        // Act
        var result = await _controller.Create(order);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as OrderForCreationDto).Should().BeEquivalentTo(order);

        _mediatorMock.Verify(m => m.Send(new CreateOrderCommand(order), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateOrderCommand(It.IsAny<OrderForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingOrder_ReturnsNoContentResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new OrderForUpdateDto { Id = orderId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOrderCommand(order), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(orderId, order);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateOrderCommand(order), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingOrder_ReturnsNotFoundResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new OrderForUpdateDto { Id = orderId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOrderCommand(order), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(orderId, order);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateOrderCommand(order), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(orderId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateOrderCommand(It.IsAny<OrderForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingOrderId_ReturnsNoContentResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOrderCommand(orderId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteOrderCommand(orderId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingOrderId_ReturnsNotFoundResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOrderCommand(orderId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(orderId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteOrderCommand(orderId), CancellationToken.None), Times.Once);
    }
}

