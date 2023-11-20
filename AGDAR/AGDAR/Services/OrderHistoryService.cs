using AGDAR.Models;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;

namespace AGDAR.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly OrderHistoryRepository _orderHistoryRepository;

        public OrderHistoryService(OrderHistoryRepository orderHistoryRepository) //Constructor
        {
            _orderHistoryRepository = orderHistoryRepository;

        }

        public bool Update(int id, OrderHistory orderHistory) // Update
        {
            var orderHistoryToUpdate = _orderHistoryRepository.GetById(id);
            if (orderHistory is null)
            {
                return false;
            }
            orderHistoryToUpdate.ClientId = orderHistory.ClientId;
            orderHistoryToUpdate.OrderId = orderHistory.OrderId;
            orderHistoryToUpdate.ClientName = orderHistory.ClientName;
            orderHistoryToUpdate.Price = orderHistory.Price;
            orderHistoryToUpdate.OrderEndDate = orderHistory.OrderEndDate;

            _orderHistoryRepository.UpdateAndSaveChanges(orderHistoryToUpdate);
            return true;
        }

        public bool Delete(int id) // Delete
        {
            var orderHistoryToDelete = _orderHistoryRepository.GetById(id);
            if (orderHistoryToDelete is null)
            {
                return false;
            }
            _orderHistoryRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }
        public OrderHistory GetById(int id)   //GetById
        {
            var orderHistory = _orderHistoryRepository.GetById(id);
            if (orderHistory is null)
            {
                return null;
            }
            return orderHistory;
        }
        public List<OrderHistory> GetAll() //GetAll
        {
            var orderHistory = _orderHistoryRepository.GetAll().ToList();
            return orderHistory;
        }
        public int Create(OrderHistory orderHistory) //Create
        {
            _orderHistoryRepository.AddAndSaveChanges(orderHistory);

            return orderHistory.Id;
        }

    }
}
