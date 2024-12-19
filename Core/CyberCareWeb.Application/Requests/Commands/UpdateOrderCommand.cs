using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record UpdateOrderCommand(OrderForUpdateDto Order) : IRequest<bool>;
