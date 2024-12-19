using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record UpdateOrderComponentCommand(OrderComponentForUpdateDto OrderComponent) : IRequest<bool>;
