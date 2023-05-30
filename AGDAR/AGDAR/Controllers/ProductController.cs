using AGDAR.Models;
using AGDAR.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Controllers
{
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly AGDARDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductController(AGDARDbContext dbContext, IMapper mapper) // Konstruktor
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateProduct([FromBody]CreateProductDto dto) // Create
        {
            var product = _mapper.Map<Product>(dto);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Created($"/api/product/{product.Id}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll() // GetAll
        {
            var products = _dbContext.Products.ToList();

            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get([FromRoute]int id) // Get
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
    }
}
