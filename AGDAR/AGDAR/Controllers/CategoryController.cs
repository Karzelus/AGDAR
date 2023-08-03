using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Controllers
{
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) // Konstruktor
        {
            _categoryService = categoryService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CategoryDto dto, [FromRoute] int id) //Edit
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _categoryService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpDelete("{id}")] // Delete
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _categoryService.Delete(id);

            if (isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateCategory([FromBody] CategoryDto dto) // Create
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _categoryService.Create(dto);
            return Created($"/api/category/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAll() // GetAll
        {
            var categorysDtos = _categoryService.GetAll();
            return Ok(categorysDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get([FromRoute] int id) // Get
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
