using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class CreateComponentTypeCommandHandler : IRequestHandler<CreateComponentTypeCommand>
{
	private readonly IComponentTypeRepository _repository;
	private readonly IMapper _mapper;

	public CreateComponentTypeCommandHandler(IComponentTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateComponentTypeCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<ComponentType>(request.ComponentType));
		await _repository.SaveChanges();
	}
}
