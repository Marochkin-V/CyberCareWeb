using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetOrderComponentByIdQueryHandler : IRequestHandler<GetOrderComponentByIdQuery, OrderComponentDto?>
{
	private readonly IOrderComponentRepository _repository;
	private readonly IMapper _mapper;

	public GetOrderComponentByIdQueryHandler(IOrderComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OrderComponentDto?> Handle(GetOrderComponentByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<OrderComponentDto>(await _repository.GetById(request.Id, trackChanges: false));
}
