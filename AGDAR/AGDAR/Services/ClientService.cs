using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using AGDAR.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AGDAR.Services
{
    public class ClientService : IClientService
    {
        private readonly ClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientService(ClientRepository clientRepository, IMapper mapper) //Constructor
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public bool Update(int id, ClientDto dto) // Update
        {
            var client = _clientRepository.GetById(id);
            if (client is null)
            {
                return false;
            }
            client.Name = dto.Name;
            client.SeckondName = dto.SeckondName;
            client.Email = dto.Email;
            client.DateOfBirth = dto.DateOfBirth;
            client.Password = dto.Password;
            client.OrderdId = dto.OrderdId;

            _clientRepository.UpdateAndSaveChanges(client);
            return true;
        }

        public bool Delete(int id) // Delete
        {
            var client = _clientRepository.GetById(id);
            if (client is null)
            {
                return false;
            }
            _clientRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }
        public ClientDto GetById(int id)   //GetById
        {
            var client = _clientRepository.GetById(id);
            if (client is null)
            {
                return null;
            }
            var clientDto = _mapper.Map<ClientDto>(client);
            clientDto.OrderdId = client.OrderdId;
            return clientDto;
        }

        public List<ClientDto> GetAll() //GetAll
        {
            var clients = _clientRepository.GetAll().ToList();
            var clientsDtos = _mapper.Map<List<ClientDto>>(clients);
            int num = 0;
            foreach(var client in clientsDtos)
            {
                client.OrderdId = clients[num].OrderdId;
                num++;
            }
            return clientsDtos;
        }

        public int Create(ClientDto dto) //Create
        {
            var client = _mapper.Map<Client>(dto);
            _clientRepository.AddAndSaveChanges(client);

            return client.Id;
        }
    }
}
