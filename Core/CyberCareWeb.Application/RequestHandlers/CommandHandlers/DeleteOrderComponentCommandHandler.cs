﻿using MediatR;
using CyberCareWeb.Domain.Abstractions;
using CyberCareWeb.Application.Requests.Commands;

namespace CyberCareWeb.Application.RequestHandlers.CommandHandlers;

public class DeleteOrderComponentCommandHandler(IOrderComponentRepository repository) : IRequestHandler<DeleteOrderComponentCommand, bool>
{
	private readonly IOrderComponentRepository _repository = repository;

	public async Task<bool> Handle(DeleteOrderComponentCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
