using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetComponentTypesQuery : IRequest<IEnumerable<ComponentTypeDto>>;
