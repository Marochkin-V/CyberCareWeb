using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetComponentTypesQuery : IRequest<PagedResult<ComponentTypeDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public GetComponentTypesQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}