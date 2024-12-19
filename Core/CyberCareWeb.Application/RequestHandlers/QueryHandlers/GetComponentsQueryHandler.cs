using MediatR;
using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Queries;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers;

public class GetComponentsQueryHandler : IRequestHandler<GetComponentsQuery, PagedResult<ComponentDto>>
{
	private readonly IComponentRepository _repository;
	private readonly IMapper _mapper;

	public GetComponentsQueryHandler(IComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	//public async Task<IEnumerable<ComponentDto>> Handle(GetComponentsQuery request, CancellationToken cancellationToken) => 
	//	_mapper.Map<IEnumerable<ComponentDto>>(await _repository.Get(trackChanges: false));

    public async Task<PagedResult<ComponentDto>> Handle(GetComponentsQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync();
        var components = await _repository.GetPageAsync(request.Page, request.PageSize);

        var items = _mapper.Map<IEnumerable<ComponentDto>>(components);
        return new PagedResult<ComponentDto>(items, totalItems, request.Page, request.PageSize);
    }
}
