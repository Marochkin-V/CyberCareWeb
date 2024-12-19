using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record UpdateComponentTypeCommand(ComponentTypeForUpdateDto ComponentType) : IRequest<bool>;
