using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class UpdateComponentTypeCommandHandler : IRequestHandler<UpdateComponentTypeCommand, bool>
{
	private readonly IComponentTypeRepository _repository;
	private readonly IMapper _mapper;

	public UpdateComponentTypeCommandHandler(IComponentTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateComponentTypeCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.ComponentType.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.ComponentType, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
