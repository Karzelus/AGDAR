using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Controllers;
using AGDAR.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AGDAR.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly WorkerRepository _workerRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Worker> _passwordHasherWorker;
        public WorkerService(WorkerRepository workerRepository, IMapper mapper, IPasswordHasher<Worker> passwordHasherWorker) //Constructor
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
            _passwordHasherWorker = passwordHasherWorker;
        }
        public bool Update(int id, WorkerDto dto) // Update
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
            worker.Password = worker.Password;
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

        public int Create(WorkerDto dto) //Create
        {
            var worker = _mapper.Map<Worker>(dto);
            _workerRepository.AddAndSaveChanges(worker);

            return worker.Id;
        }
    }
}
