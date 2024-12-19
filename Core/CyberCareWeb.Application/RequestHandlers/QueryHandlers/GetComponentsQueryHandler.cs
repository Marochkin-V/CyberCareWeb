using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetComponentsQueryHandler : IRequestHandler<GetComponentsQuery, IEnumerable<ComponentDto>>
{
	private readonly IComponentRepository _repository;
	private readonly IMapper _mapper;

	public GetComponentsQueryHandler(IComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ComponentDto>> Handle(GetComponentsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<ComponentDto>>(await _repository.Get(trackChanges: false));
}
