using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Services.Interfaces;
using AGDAR.Services;

namespace AGDAR.Controllers
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) // Konstruktor
        {
            _orderService = orderService;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] OrderDto dto, [FromRoute] int id) //Edit
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _orderService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();

        }
        [HttpDelete("{id}")] // Delete
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _orderService.Delete(id);

            if (isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateOrder([FromBody]CreateOrderDto dto) // Create
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _orderService.Create(dto);
            return Created($"/api/category/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll() // GetAll
        {
            var ordersDtos = _orderService.GetAll();
            return Ok(ordersDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> Get([FromRoute] int id) // Get
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}