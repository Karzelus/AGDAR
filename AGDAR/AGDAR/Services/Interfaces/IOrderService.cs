using AGDAR.Models;
using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IOrderService
    {
        bool Update(int id, OrderDto dto);
        bool Delete(int id);
        int Create(CreateOrderDto dto);
        List<OrderDto> GetAll();
        OrderDto GetById(int id);
    }
}