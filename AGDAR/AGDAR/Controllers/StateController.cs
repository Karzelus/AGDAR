using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Services.Interfaces;
using AGDAR.Services;

namespace AGDAR.Controllers
{
    [Route("api/state")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        public StateController(IStateService stateService) // Konstruktor
        {
            _stateService = stateService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<State>> GetAll() // GetAll
        {
            var state = _stateService.GetAll();

            //var role = _mapper.Map<List<OrderDto>>(order);
            return Ok(state);
        }

        [HttpGet("{id}")]
        public ActionResult<State> Get([FromRoute] int id) // Get
        {
            var state = _stateService.GetById(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
