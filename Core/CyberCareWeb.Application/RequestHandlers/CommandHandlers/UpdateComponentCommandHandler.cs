using MediatR;
using AutoMapper;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class UpdateComponentCommandHandler : IRequestHandler<UpdateComponentCommand, bool>
{
	private readonly IComponentRepository _repository;
	private readonly IMapper _mapper;

	public UpdateComponentCommandHandler(IComponentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Component.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Component, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
