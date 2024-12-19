using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
