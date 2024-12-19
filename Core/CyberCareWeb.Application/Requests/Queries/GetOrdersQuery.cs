using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetOrdersQuery : IRequest<IEnumerable<OrderDto>>;
