using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetComponentByIdQuery(Guid Id) : IRequest<ComponentDto?>;
