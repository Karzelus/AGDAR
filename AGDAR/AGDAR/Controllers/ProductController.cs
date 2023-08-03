using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Controllers
{
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) // Konstruktor
        {
            _productService = productService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateProductDto dto, [FromRoute] int id) //Edit
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _productService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) //Delete
        {
            var isDeleted = _productService.Delete(id);

            if (isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody]CreateProductDto dto) // Create
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var id = _productService.Create(dto);
            return Created($"/api/product/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll() // GetAll
        {
            var productsDtos = _productService.GetAll();
            return Ok(productsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get([FromRoute]int id) // Get
        {
            var product = _productService.GetById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
