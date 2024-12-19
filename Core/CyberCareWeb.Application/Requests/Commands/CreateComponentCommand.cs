using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record CreateComponentCommand(ComponentForCreationDto Component) : IRequest;
