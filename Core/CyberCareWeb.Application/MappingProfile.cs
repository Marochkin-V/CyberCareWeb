using AutoMapper;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Application.Dtos;

namespace CyberCareWeb.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<ComponentType, ComponentTypeDto>();
		CreateMap<ComponentTypeForCreationDto, ComponentType>();
		CreateMap<ComponentTypeForUpdateDto, ComponentType>();

		CreateMap<Component, ComponentDto>();
		CreateMap<ComponentForCreationDto, Component>();
		CreateMap<ComponentForUpdateDto, Component>();

		CreateMap<Order, OrderDto>();
		CreateMap<OrderForCreationDto, Order>();
		CreateMap<OrderForUpdateDto, Order>();

		CreateMap<OrderComponent, OrderComponentDto>();
		CreateMap<OrderComponentForCreationDto, OrderComponent>();
		CreateMap<OrderComponentForUpdateDto, OrderComponent>();
    }
}

