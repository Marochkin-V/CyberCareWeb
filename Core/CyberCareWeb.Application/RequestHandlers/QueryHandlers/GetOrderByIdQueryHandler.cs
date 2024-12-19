using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;

	public GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<OrderDto>(await _repository.GetById(request.Id, trackChanges: false));
}
