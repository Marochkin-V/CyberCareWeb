using MediatR;

namespace CyberCareWeb.Application.Requests.Commands;

public record DeleteOrderComponentCommand(Guid Id) : IRequest<bool>;
