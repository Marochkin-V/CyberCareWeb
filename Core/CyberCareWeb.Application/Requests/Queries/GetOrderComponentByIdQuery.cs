using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetOrderComponentByIdQuery(Guid Id) : IRequest<OrderComponentDto?>;
