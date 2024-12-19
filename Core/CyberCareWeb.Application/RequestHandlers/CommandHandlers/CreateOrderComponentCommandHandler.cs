using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class CreateOrderComponentCommandHandler : IRequestHandler<CreateOrderComponentCommand>
{
	private readonly IOrderComponentRepository _repository;
	private readonly IMapper _mapper;

	public CreateOrderComponentCommandHandler(IOrderComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateOrderComponentCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<OrderComponent>(request.OrderComponent));
		await _repository.SaveChanges();
	}
}
