using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;

	public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Order>(request.Order));
		await _repository.SaveChanges();
	}
}
