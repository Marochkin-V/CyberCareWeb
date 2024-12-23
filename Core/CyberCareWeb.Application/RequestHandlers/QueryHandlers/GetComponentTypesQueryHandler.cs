﻿using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetComponentTypesQueryHandler : IRequestHandler<GetComponentTypesQuery, PagedResult<ComponentTypeDto>>
{
	private readonly IComponentTypeRepository _repository;
	private readonly IMapper _mapper;

	public GetComponentTypesQueryHandler(IComponentTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	//public async Task<IEnumerable<ComponentTypeDto>> Handle(GetComponentTypesQuery request, CancellationToken cancellationToken) => 
	//	_mapper.Map<IEnumerable<ComponentTypeDto>>(await _repository.Get(trackChanges: false));
    public async Task<PagedResult<ComponentTypeDto>> Handle(GetComponentTypesQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync();
        var componentTypes = await _repository.GetPageAsync(request.Page, request.PageSize);

        var items = _mapper.Map<IEnumerable<ComponentTypeDto>>(componentTypes);
        return new PagedResult<ComponentTypeDto>(items, totalItems, request.Page, request.PageSize);
    }
}
