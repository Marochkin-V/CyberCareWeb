using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetOrderComponentsQuery : IRequest<IEnumerable<OrderComponentDto>>;
