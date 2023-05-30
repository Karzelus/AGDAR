using AGDAR.Models;
using AGDAR.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AGDAR.Controllers
{
    [Route("api/worker")]
    public class WorkerController : ControllerBase
    {       
        private readonly AGDARDbContext _dbContext;
        private readonly IMapper _mapper;
        public WorkerController(AGDARDbContext dbContext, IMapper mapper) // Konstructor
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateWorker([FromBody] CreateWorkerDto dto) // Create
        {
            var worker = _mapper.Map<Worker>(dto);
            _dbContext.Workers.Add(worker);
            _dbContext.SaveChanges();

            return Created($"/api/worker/{worker.Id}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<WorkerDto>> GetAll() // GetAll
        {
            var workers = _dbContext.Workers.ToList();

            //var workersDto = _mapper.Map<List<WorkerDto>>(workers);
            return Ok(workers);

            //var workers = _dbContext.Workers.ToList();
            //var workersDto = new List<WorkerDto>();
            //foreach (var worker in workers)
            //{
            //    var role = this._dbContext.Roles.FirstOrDefault(x => x.Id == worker.RoleId);
            //    workersDto.Add(new WorkerDto()
            //    {
            //        Id = worker.Id,
            //        Email = worker.Email,
            //        Name = worker.Name,
            //        SeckondName = worker.SeckondName,
            //        DateOfBirth = worker.DateOfBirth,
            //        Role = role.Name
            //    });
            //}
            //return Ok(workersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<WorkerDto> Get([FromRoute] int id) // Get
        {
            var worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            if (worker == null)
            {
                return NotFound();
            }
            var workerDto = _mapper.Map<ProductDto>(worker);
            return Ok(workerDto);
        }
    }
}
