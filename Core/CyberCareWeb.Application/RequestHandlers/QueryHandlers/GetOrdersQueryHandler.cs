using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedResult<OrderDto>>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;

	public GetOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	//public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken) => 
	//	_mapper.Map<IEnumerable<OrderDto>>(await _repository.Get(trackChanges: false));

    public async Task<PagedResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync();
        var orders = await _repository.GetPageAsync(request.Page, request.PageSize);

        var items = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return new PagedResult<OrderDto>(items, totalItems, request.Page, request.PageSize);
    }
}
