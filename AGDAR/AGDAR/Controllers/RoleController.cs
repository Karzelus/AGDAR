using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Services.Interfaces;

namespace AGDAR.Controllers
{
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) // Konstruktor
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAll() // GetAll
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public ActionResult<Role> Get([FromRoute] int id) // Get
        {
            var role = _roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
    }
}
