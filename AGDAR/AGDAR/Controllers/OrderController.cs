using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Services.Interfaces;
using AGDAR.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
using AGDAR.Repositories;
using Microsoft.AspNetCore.Http;
using AGDAR.Emails;

namespace AGDAR.Controllers
{
    //[Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly OrderProductRepository _orderProductRepository;
        private readonly OrderHistoryRepository _orderHistoryRepositroy;
        private readonly IClientService _clientService;
        private readonly IEmailSender _emailSeneder;
        public OrderController(IEmailSender emailSeneder ,IOrderService orderService, IProductService productService, IClientService clientService ,OrderProductRepository orderProductRepository, OrderHistoryRepository orderHistoryRepository ) // Konstruktor
        {
            _orderService = orderService;
            _productService = productService;
            _orderProductRepository = orderProductRepository;
            _orderHistoryRepositroy = orderHistoryRepository;
            _clientService = clientService;
            _emailSeneder = emailSeneder;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            List<OrderDto> order = _orderService.GetAll();
            if (order != null)
            {
                return View(order);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_orderService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
            }

            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
            }

            return View(order);
        }

        public async Task<IActionResult> CompleteOrder(int id)
        {
            if (_orderService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
            }

            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
            }

            return View(order);
        }

        public async Task<IActionResult> EndOrder(int orderId)
        {
            var order = _orderService.GetById(orderId);
            var client = _clientService.GetById(order.ClientId.GetValueOrDefault());
           
            var orderHistory = new OrderHistory
            {
                OrderId = order.Id,
                ClientId = order.ClientId,
                ClientName = client.Name + " " + client.SeckondName,
                Price = order.Price,
                OrderEndDate = DateTime.Now
            };
            var orderProducts = _orderProductRepository.GetAll().Where(x => x.OrderId == orderId).ToList();
            List<ProductDto> products = new List<ProductDto>();
            foreach(var product in orderProducts)
            {
                products.Add( _productService.GetById(product.ProductId));
            }

            _orderHistoryRepositroy.AddAndSaveChanges(orderHistory); //Tworzymy rekord w zakończonych zamówieniach  
            var newOrder = new CreateOrderDto
            {
                Description = "",
                Price = 0,
                ClientId = client.Id
            };
            int newOrderId = _orderService.Create(newOrder); //Dajemy nowe zamówienie do tabeli zamówienia i dla użytkonika

            var subject = "Podsumowanie zamówienia";
            var message = "Dziękujemy za dokonanie zakupów w naszym sklepie\n Zawartość zamówienia: \n";
            int num = 1;
            foreach (var product in products)
            {
                message += num + ". Nazwa: " + product.Name + " Cena: " + product.Price +  " zł\n";
                num++;
            }
            message += "Kwota końcowa: " + order.Price + " zł";

            _emailSeneder.SendEmailAsync(client.Email, subject, message);

            client.OrderdId = newOrderId;
            _clientService.Update(client.Id,client);
            return Redirect("/Products");
        }


        //// GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Price,ClientId")] CreateOrderDto order)
        {
            if (ModelState.IsValid)
            {
                _orderService.Create(order);
                //await _categoryService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        //// GET: Order/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_orderService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Orders'  is null.");
            }

            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Orders'  is null.");
            }
            return View(order);
        }

        //// POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Price,Name")] OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _orderService.Update(id, order);
            if (!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.Orders'  is null.");
            }
            return Redirect("/Order");
        }

        //// GET: Order/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_orderService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
            }

            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
            }

            return View(order);
        }
        //// POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _orderService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.Order'  is null.");
        }

        private bool orderxists(int id)
        {
            return (_orderService.GetAll()?.Any(o => o.Id == id)).GetValueOrDefault();
        }      
    }
}