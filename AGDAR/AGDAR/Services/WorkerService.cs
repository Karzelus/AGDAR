using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Controllers;
using AGDAR.Repositories;

namespace AGDAR.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly WorkerRepository _workerRepository;
        private readonly IMapper _mapper;
        public WorkerService(WorkerRepository workerRepository, IMapper mapper) //Constructor
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
        }
        public bool Update(int id, CreateWorkerDto dto) // Update
        {
            var worker = _workerRepository.GetById(id);
            if (worker is null)
            {
                return false;
            }
            worker.Name = dto.Name;
            worker.SeckondName = dto.SeckondName;
            worker.Email = dto.Email;
            worker.DateOfBirth = dto.DateOfBirth;
            worker.Password = dto.Password;
            worker.RoleId = dto.RoleId;

            _workerRepository.UpdateAndSaveChanges(worker);
            return true;
        }
        public bool Delete(int id) // Delete
        {
            var worker = _workerRepository.GetById(id);
            if (worker is null)
            {
                return false;
            }
            _workerRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }
        public WorkerDto GetById(int id)   //GetById
        {
            var worker = _workerRepository.GetById(id);
            if (worker is null)
            {
                return null;
            }
            var workerDto = _mapper.Map<WorkerDto>(worker);
            return workerDto;
        }

        public List<WorkerDto> GetAll() //GetAll
        {
            var workers = _workerRepository.GetAll().ToList();
            var workersDtos = _mapper.Map<List<WorkerDto>>(workers);
            return workersDtos;

        }

        public int Create(CreateWorkerDto dto) //Create
        {
            var worker = _mapper.Map<Worker>(dto);
            _workerRepository.AddAndSaveChanges(worker);

            return worker.Id;
        }
    }
}
