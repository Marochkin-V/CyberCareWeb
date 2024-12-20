using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCareWeb.Application.Requests.Queries
{
    public record GetComponentTypesAllQuery : IRequest<IEnumerable<ComponentTypeDto>>;
}