using MediatR;

namespace CyberCareWeb.Application.Requests.Commands;

public record DeleteComponentCommand(Guid Id) : IRequest<bool>;
