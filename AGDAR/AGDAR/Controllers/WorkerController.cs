using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace AGDAR.Controllers
{
    [Route("api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        public WorkerController(IWorkerService workerService) // Konstructor
        {
            _workerService = workerService;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody]CreateWorkerDto dto, [FromRoute]int id) //Edit
        { 
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _workerService.Update(id, dto);
            if(!isUpdated)
            {
                return NotFound();
            }
            return Ok();
            
        }
        [HttpDelete("{id}")] // Delete
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _workerService.Delete(id);

            if (isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateWorker([FromBody]CreateWorkerDto dto) // Create
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _workerService.Create(dto);
            return Created($"/api/worker/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkerDto>> GetAll() // GetAll
        {
            var workersDto = _workerService.GetAll();
            return Ok(workersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<WorkerDto> Get([FromRoute] int id) // Get
        {
            var worker = _workerService.GetById(id);
            if (worker == null)
            {
                return NotFound();
            }
            return Ok(worker);
        }
    }
}
