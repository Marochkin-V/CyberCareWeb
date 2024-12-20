using AutoMapper;
using CyberCareWeb.Application.Dtos;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCareWeb.Application.RequestHandlers.QueryHandlers
{
    public class GetComponentTypesAllQueryHandler : IRequestHandler<GetComponentTypesAllQuery, IEnumerable<ComponentTypeDto>>
    {
        private readonly IComponentTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetComponentTypesAllQueryHandler(IComponentTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComponentTypeDto>> Handle(GetComponentTypesAllQuery request, CancellationToken cancellationToken) =>
            _mapper.Map<IEnumerable<ComponentTypeDto>>(await _repository.Get(trackChanges: false));
    }
}
