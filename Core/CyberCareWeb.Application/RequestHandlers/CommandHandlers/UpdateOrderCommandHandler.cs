using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;

	public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Order.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Order, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
