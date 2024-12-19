using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetOrderComponentsQueryHandler : IRequestHandler<GetOrderComponentsQuery, IEnumerable<OrderComponentDto>>
{
	private readonly IOrderComponentRepository _repository;
	private readonly IMapper _mapper;

	public GetOrderComponentsQueryHandler(IOrderComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<OrderComponentDto>> Handle(GetOrderComponentsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<OrderComponentDto>>(await _repository.Get(trackChanges: false));
}
