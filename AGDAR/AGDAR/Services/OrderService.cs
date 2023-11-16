using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using AGDAR.Repositories;

namespace AGDAR.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly OrderProductRepository _orderProductRepository;
        public OrderService(OrderRepository orderRepository, IMapper mapper, OrderProductRepository orderProductRepository, ProductRepository productRepository) //Constructor
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
        }
        public bool Update(int id, OrderDto dto) // Update
        {
            var order = _orderRepository.GetById(id);
            if (order is null)
            {
                return false;
            }
            //order.Products = dto.Products;
            order.Description = dto.Description;
            order.Price = dto.Price;

            _orderRepository.UpdateAndSaveChanges(order);
            return true;
        }
        public bool Delete(int id) // Delete
        {
            var order = _orderRepository.GetById(id);
            if (order is null)
            {
                return false;
            }
            _orderRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }
        public OrderDto GetById(int id)   //GetById
        {
            var order = _orderRepository.GetById(id);
            var orderDto = _mapper.Map<OrderDto>(order);
            var orderProduct = _orderProductRepository.GetAll().Where(op => op.OrderId == id).ToList();
            foreach(var product in orderProduct)
            {
                orderDto.Products.Add(_productRepository.GetById(product.ProductId));
            }
            return orderDto;
        }

        public List<OrderDto> GetAll() //GetAll
        {
            var orders = _orderRepository.GetAll().ToList();
            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);
            return ordersDtos;

        }

        public int Create(CreateOrderDto dto) //Create
        {
            var order = _mapper.Map<Order>(dto);
            _orderRepository.AddAndSaveChanges(order);

            return order.Id;
        }
    }
}
