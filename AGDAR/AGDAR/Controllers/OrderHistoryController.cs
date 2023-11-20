using AGDAR.Models;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly IOrderHistoryService _orderHistoryService;
        public OrderHistoryController(IOrderHistoryService orderHistoryService)
        {
            _orderHistoryService = orderHistoryService;
        }

        // GET: OrderHistory
        public async Task<IActionResult> Index()
        {
            List<OrderHistory> orderHistory = _orderHistoryService.GetAll();
            if (orderHistory != null)
            {
                return View(orderHistory);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
        }
        // GET: OrderHistory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_orderHistoryService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }

            var orderHistory = _orderHistoryService.GetById(id);
            if (orderHistory == null)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }

            return View(orderHistory);
        }
        //// GET: OrderHistory/Create
        public IActionResult Create()
        {
            return View();
        }
        //// POST: OrderHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ClientName,Price,OrderEndDate,OrderId")] OrderHistory orderHistory)
        {
            if (ModelState.IsValid)
            {
                _orderHistoryService.Create(orderHistory);
                //await _clientService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(orderHistory);
        }
        // GET: OrderHistory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_orderHistoryService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }

            var orderHistory = _orderHistoryService.GetById(id);
            if (orderHistory == null)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }
            return View(orderHistory);
        }
        //// POST: OrderHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ClientName,Price,OrderEndDate,OrderId")] OrderHistory orderHistory)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

            var isUpdated = _orderHistoryService.Update(id, orderHistory);
            if (!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }
            return View(orderHistory);
        }
        //// GET: OrderHistory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_orderHistoryService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }

            var orderHistory = _orderHistoryService.GetById(id);
            if (orderHistory == null)
            {
                return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");
            }

            return View(orderHistory);
        }

        //// POST: OrderHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _orderHistoryService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.OrderHistory'  is null.");

        }

        private bool OrderHistoryExists(int id)
        {
            return (_orderHistoryService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
