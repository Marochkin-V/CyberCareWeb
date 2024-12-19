using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record CreateOrderComponentCommand(OrderComponentForCreationDto OrderComponent) : IRequest;
