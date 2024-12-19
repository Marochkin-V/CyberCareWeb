using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetComponentByIdQueryHandler : IRequestHandler<GetComponentByIdQuery, ComponentDto?>
{
	private readonly IComponentRepository _repository;
	private readonly IMapper _mapper;

	public GetComponentByIdQueryHandler(IComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ComponentDto?> Handle(GetComponentByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ComponentDto>(await _repository.GetById(request.Id, trackChanges: false));
}
