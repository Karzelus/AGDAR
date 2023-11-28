using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Models.DTOs;
using AutoMapper;

namespace AGDAR.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateCustomProductDto, Product>();
            CreateMap<Product, CreateCustomProductDto>();

            CreateMap<Worker, WorkerDto>()
                .ForMember(m => m.Role, c => c.MapFrom(s => s.Role.Name));
            CreateMap<WorkerDto, Worker>();

            CreateMap<Client, ClientDto>()
               .ForMember(m => m.OrderdId, c => c.MapFrom(s => s.Order.Id));
            CreateMap<ClientDto, Client>();

            CreateMap<Part, PartDto>();
            CreateMap<PartDto, Part>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
        }
    }
}
