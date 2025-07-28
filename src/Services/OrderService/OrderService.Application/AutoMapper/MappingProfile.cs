using AutoMapper;
using OrderService.Application.Models;
using OrderService.Application.Models.Requests;
using OrderService.Domain.Entities;

namespace OrderService.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressDto>();

        CreateMap<BaseEntity, BaseDto>();

        CreateMap<Country, CountryDto>();
        CreateMap<Customer, CustomerDto>();

        CreateMap<District, DistrictDto>();

        CreateMap<Neighborhood, NeighborhoodDto>();

        CreateMap<Order, OrderDto>();
        CreateMap<Order, Order>();
        CreateMap<OrderItem, OrderItem>();
        CreateMap<Order, OrderRequestModel>().ReverseMap();

        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<Province, ProvinceDto>();


    }
}