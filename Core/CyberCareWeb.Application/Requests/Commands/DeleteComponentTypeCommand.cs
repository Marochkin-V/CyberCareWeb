using MediatR;

namespace CyberCareWeb.Application.Requests.Commands;

public record DeleteComponentTypeCommand(Guid Id) : IRequest<bool>;
