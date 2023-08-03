using AGDAR.Models;
using AGDAR.Models.DTO;
using AutoMapper;

namespace AGDAR.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Worker, WorkerDto>()
                .ForMember(m => m.Role, c => c.MapFrom(s => s.Role.Name));

            CreateMap<CreateWorkerDto, Worker>();

            CreateMap<Client, ClientDto>()
               .ForMember(m => m.OrderdId, c => c.MapFrom(s => s.Order.Id));

            CreateMap<CreateClientDto, Client>();

            CreateMap<Part, PartDto>();
            CreateMap<CreatePartDto, Part>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
        }
    }
}
