using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AGDAR.Controllers
{
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService) // Konstructor
        {
            _clientService = clientService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateClientDto dto, [FromRoute] int id) //Edit
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _clientService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")] // Delete
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _clientService.Delete(id);

            if (isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateClient([FromBody] CreateClientDto dto) // Create
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _clientService.Create(dto);

            return Created($"/api/client/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> GetAll() // GetAll
        {
            var clientsDtos = _clientService.GetAll();
            return Ok(clientsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> Get([FromRoute] int id) // Get
        {
            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
    }
}
