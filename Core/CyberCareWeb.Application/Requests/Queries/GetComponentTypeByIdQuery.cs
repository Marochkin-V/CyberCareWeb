using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetComponentTypeByIdQuery(Guid Id) : IRequest<ComponentTypeDto?>;
