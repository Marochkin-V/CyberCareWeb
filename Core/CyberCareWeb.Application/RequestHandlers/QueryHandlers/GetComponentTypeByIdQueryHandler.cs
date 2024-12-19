using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetComponentTypeByIdQueryHandler : IRequestHandler<GetComponentTypeByIdQuery, ComponentTypeDto?>
{
	private readonly IComponentTypeRepository _repository;
	private readonly IMapper _mapper;

	public GetComponentTypeByIdQueryHandler(IComponentTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ComponentTypeDto?> Handle(GetComponentTypeByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<ComponentTypeDto>(await _repository.GetById(request.Id, trackChanges: false));
}
