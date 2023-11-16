using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Services.Interfaces;
using AGDAR.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
using AGDAR.Repositories;

namespace AGDAR.Controllers
{
    //[Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly OrderProductRepository _orderProductRepository;
        public OrderController(IOrderService orderService, IProductService productService, OrderProductRepository orderProductRepository ) // Konstruktor
        {
            _orderService = orderService;
            _productService = productService;
            _orderProductRepository = orderProductRepository;
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




        //[HttpPut("{id}")]
        //public ActionResult Update([FromBody] OrderDto dto, [FromRoute] int id) //Edit
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var isUpdated = _orderService.Update(id, dto);
        //    if (!isUpdated)
        //    {
        //        return NotFound();
        //    }
        //    return Ok();

        //}
        //[HttpDelete("{id}")] // Delete
        //public ActionResult Delete([FromRoute] int id)
        //{
        //    var isDeleted = _orderService.Delete(id);

        //    if (isDeleted) return NoContent();
        //    return NotFound();
        //}

        //[HttpPost]
        //public ActionResult CreateOrder([FromBody]CreateOrderDto dto) // Create
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var id = _orderService.Create(dto);
        //    return Created($"/api/category/{id}", null);
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<OrderDto>> GetAll() // GetAll
        //{
        //    var ordersDtos = _orderService.GetAll();
        //    return Ok(ordersDtos);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<OrderDto> Get([FromRoute] int id) // Get
        //{
        //    var order = _orderService.GetById(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(order);
        //}
    }
}