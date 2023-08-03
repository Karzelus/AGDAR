using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Services.Interfaces;
using AGDAR.Services;

namespace AGDAR.Controllers
{
    [Route("api/part")]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;

            public PartController(IPartService partService) // Konstruktor
            {
            _partService = partService;
            }

            [HttpPut("{id}")]
            public ActionResult Update([FromBody] CreatePartDto dto, [FromRoute] int id) //Edit
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _partService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();

            }
        [HttpDelete("{id}")] // Delete
            public ActionResult Delete([FromRoute] int id)
            {
            var isDeleted = _partService.Delete(id);

            if (isDeleted) return NoContent();
            return NotFound();
            }
        [HttpPost]
            public ActionResult CreatePart([FromBody] CreatePartDto dto) // Create
            {
                if (!ModelState.IsValid)
                {
                return BadRequest(ModelState);
                }

                var id = _partService.Create(dto);
                return Created($"/api/part/{id}", null);
            }
            [HttpGet]
            public ActionResult<IEnumerable<PartDto>> GetAll() // GetAll
            {
                var partsDtos = _partService.GetAll();
                return Ok(partsDtos);
            }

            [HttpGet("{id}")]
            public ActionResult<PartDto> Get([FromRoute] int id) // Get
            {
                var part = _partService.GetById(id);
                if (part == null)
                {
                    return NotFound();
                }
                return Ok(part);
            }       
    }
}
