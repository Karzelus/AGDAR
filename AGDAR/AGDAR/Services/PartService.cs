using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Services
{
    public class PartService : IPartService
    {
        private readonly AGDARDbContext _dbContext;
        private readonly IMapper _mapper;
        public PartService(AGDARDbContext dbContext, IMapper mapper) //Constructor
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool Update(int id, CreatePartDto dto) // Update
        {
            var part = _dbContext.Parts.FirstOrDefault(x => x.Id == id);
            if (part is null)
            {
                return false;
            }
            part.Name = dto.Name;
            part.Description = dto.Description;
            part.Price = dto.Price;

            _dbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id) // Delete
        {
            var part = _dbContext.Parts.FirstOrDefault(x => x.Id == id);
            if (part is null)
            {
                return false;
            }
            _dbContext.Parts.Remove(part);
            _dbContext.SaveChanges();
            return true;
        }
        public PartDto GetById(int id)   //GetById
        {
            var part = _dbContext.Parts.FirstOrDefault(x => x.Id == id);
            if (part is null)
            {
                return null;
            }
            var partDto = _mapper.Map<PartDto>(part);
            return partDto;
        }

        public IEnumerable<PartDto> GetAll() //GetAll
        {
            var parts = _dbContext.Parts.ToList();
            var partsDtos = _mapper.Map<List<PartDto>>(parts);
            return partsDtos;

        }

        public int Create(CreatePartDto dto) //Create
        {
            var part = _mapper.Map<Part>(dto);
            _dbContext.Parts.Add(part);
            _dbContext.SaveChanges();

            return part.Id;
        }
    }

}
