using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Queries;

public record GetOrdersQuery : IRequest<PagedResult<OrderDto>>
{
    public int Page { get; }
    public int PageSize { get; }
    public GetOrdersQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
