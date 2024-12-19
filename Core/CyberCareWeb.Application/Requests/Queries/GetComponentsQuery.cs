using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetComponentsQuery : IRequest<IEnumerable<ComponentDto>>;
