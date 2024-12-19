using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class UpdateOrderComponentCommandHandler : IRequestHandler<UpdateOrderComponentCommand, bool>
{
	private readonly IOrderComponentRepository _repository;
	private readonly IMapper _mapper;

	public UpdateOrderComponentCommandHandler(IOrderComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateOrderComponentCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.OrderComponent.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.OrderComponent, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
