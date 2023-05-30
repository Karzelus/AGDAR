using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Controllers
{
    [Route("api/part")]
    public class PartController : ControllerBase
    {       
            private readonly AGDARDbContext _dbContext;
            private readonly IMapper _mapper;
            public PartController(AGDARDbContext dbContext, IMapper mapper) // Konstruktor
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            [HttpPost]
            public ActionResult CreatePart([FromBody] CreatePartDto dto) // Create
            {
                var part = _mapper.Map<Part>(dto);
                _dbContext.Parts.Add(part);
                _dbContext.SaveChanges();

                return Created($"/api/part/{part.Id}", null);
            }
            [HttpGet]
            public ActionResult<IEnumerable<PartDto>> GetAll() // GetAll
            {
                var parts = _dbContext.Parts.ToList();

                var partsDto = _mapper.Map<List<PartDto>>(parts);
                return Ok(partsDto);
            }

            [HttpGet("{id}")]
            public ActionResult<PartDto> Get([FromRoute] int id) // Get
            {
                var part = _dbContext.Parts.FirstOrDefault(x => x.Id == id);
                if (part == null)
                {
                    return NotFound();
                }
                var partDto = _mapper.Map<PartDto>(part);
                return Ok(partDto);
            }       
    }
}
