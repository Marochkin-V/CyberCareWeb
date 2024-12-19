using MediatR;

namespace CyberCareWeb.Application.Requests.Commands;

public record DeleteOrderCommand(Guid Id) : IRequest<bool>;
