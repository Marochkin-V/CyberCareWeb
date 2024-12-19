using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetComponentsQuery : IRequest<PagedResult<ComponentDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public GetComponentsQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
