using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using AGDAR.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace AGDAR.Services
{
    public class ClientService : IClientService
    {
        private readonly ClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Client> _passwordHasherClient;
        public ClientService(ClientRepository clientRepository, IMapper mapper, IPasswordHasher<Client> passwordHasherClient) //Constructor
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _passwordHasherClient = passwordHasherClient;
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

        public bool UpdateClient(int id, Client dto) // Update
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

        public Client GetClientById(int id)   //GetById
        {
            var client = _clientRepository.GetById(id);
            if (client is null)
            {
                return null;
            }

            return client;
        }

        public bool ChangePassword(int id, ClientDto dto)
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
            client.OrderdId = dto.OrderdId;

            var hashedPassword = _passwordHasherClient.HashPassword(client, dto.Password);
            client.Password = hashedPassword;

            _clientRepository.UpdateAndSaveChanges(client);
            return true;
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
