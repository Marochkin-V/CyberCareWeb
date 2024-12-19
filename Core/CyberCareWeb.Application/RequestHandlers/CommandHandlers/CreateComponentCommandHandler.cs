using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class CreateComponentCommandHandler : IRequestHandler<CreateComponentCommand>
{
	private readonly IComponentRepository _repository;
	private readonly IMapper _mapper;

	public CreateComponentCommandHandler(IComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateComponentCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Component>(request.Component));
		await _repository.SaveChanges();
	}
}
