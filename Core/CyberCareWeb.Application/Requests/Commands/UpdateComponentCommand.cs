using MediatR;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application.Requests.Commands;

public record UpdateComponentCommand(ComponentForUpdateDto Component) : IRequest<bool>;
