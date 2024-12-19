using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record CreateComponentTypeCommand(ComponentTypeForCreationDto ComponentType) : IRequest;
