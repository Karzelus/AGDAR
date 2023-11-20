using AGDAR.Models;
namespace AGDAR.Services.Interfaces
{
    public interface IOrderHistoryService
    {
        bool Update(int id, OrderHistory orderHistory);
        bool Delete(int id);
        int Create(OrderHistory dto);
        List<OrderHistory> GetAll();
        OrderHistory GetById(int id);
    }
}
