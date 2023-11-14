using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Models.DTOs;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AGDAR.Services
{
    public class AccountService : IAccountService
    {
        private readonly AGDARDbContext _context;
        private readonly WorkerRepository _workerRepository;
        private readonly ClientRepository _clientRepository;
        private readonly IPasswordHasher<Worker> _passwordHasherWorker;
        private readonly IPasswordHasher<Client> _passwordHasherClient;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;

        public AccountService(AGDARDbContext context, IMapper mapper, WorkerRepository workerRepository, ClientRepository clientRepository, IPasswordHasher<Worker> passwordHasherWorker, IPasswordHasher<Client> passwordHasherClient, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _workerRepository = workerRepository;
            _clientRepository = clientRepository;
            _passwordHasherWorker = passwordHasherWorker;
            _passwordHasherClient = passwordHasherClient;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }
        public void RegisterClient(ClientDto dto) //Rejestracja Klienta
        {
            var newClient = new Client()
            {
                Email = dto.Email,
                Name = dto.Name,
                SeckondName = dto.SeckondName,
                DateOfBirth = dto.DateOfBirth
            };
            var hashedPassword = _passwordHasherClient.HashPassword(newClient, dto.Password);
            newClient.Password = hashedPassword;
            _clientRepository.AddAndSaveChanges(newClient);
        }
        public void RegisterWorker(WorkerDto dto) //Rejestracja Pracownika
        {
            var newWorker = new Worker()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Name = dto.Name,
                SeckondName = dto.SeckondName,
                RoleId = dto.RoleId,
            };
            var hashedPassword = _passwordHasherWorker.HashPassword(newWorker, dto.Password);
            newWorker.Password = hashedPassword;
            _workerRepository.AddAndSaveChanges(newWorker);
        }
        public string GenerateJwt(HttpContext httpContext, LoginDto dto)
        {
            var worker = _context.Workers.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.Email);
                if(worker.Email is null)
                {
                    throw (new Exception());
                }
                var result = _passwordHasherWorker.VerifyHashedPassword(worker, worker.Password, dto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    throw (new Exception());
                } 

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, worker.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{worker.Name} {worker.SeckondName}"),
                    new Claim(ClaimTypes.Role, $"{worker.Role.Name}")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.jwtIssuer,
                    _authenticationSettings.jwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Issuer = _authenticationSettings.jwtIssuer,
                Audience = _authenticationSettings.jwtIssuer,
                Expires = expires,
                SigningCredentials = cred,
                Subject = new ClaimsIdentity(claims)
            });

            var jwtTokeSession = tokenHandler.WriteToken(jwtToken);
            //httpContext.Session.SetString("jwtToken", jwtTokeSession);
            httpContext.Session.SetString("WorkerEmail", worker.Email);
            httpContext.Session.SetString("WorkerId", worker.Id.ToString());
            return jwtTokeSession;
        }
        public WorkerDto GetWorkerByEmail(string email)   //GetById
        {
            var worker = _workerRepository.GetByEmail(email);
            if (worker is null)
            {
                return null;
            }
            var workerDto = _mapper.Map<WorkerDto>(worker);
            return workerDto;
        }

        public string GenerateClientJwt(HttpContext httpContext, LoginDto dto)
        {
            var client = _context.Clients.FirstOrDefault(u => u.Email == dto.Email);
            if (client.Email is null)
            {
                throw (new Exception());
            }
            var result = _passwordHasherClient.VerifyHashedPassword(client, client.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw (new Exception());
            }

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{client.Name} {client.SeckondName}")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.jwtIssuer,
                    _authenticationSettings.jwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Issuer = _authenticationSettings.jwtIssuer,
                Audience = _authenticationSettings.jwtIssuer,
                Expires = expires,
                SigningCredentials = cred,
                Subject = new ClaimsIdentity(claims)
            });

            var jwtTokeSession = tokenHandler.WriteToken(jwtToken);
            //httpContext.Session.SetString("jwtToken", jwtTokeSession);
            httpContext.Session.SetString("ClientEmail", client.Email);
            httpContext.Session.SetString("ClientId", client.Id.ToString());
            return jwtTokeSession;
        }
        public ClientDto GetClientByEmail(string email)   //GetById
        {
            var client = _clientRepository.GetByEmail(email);
            if (client is null)
            {
                return null;
            }
            var clientDto = _mapper.Map<ClientDto>(client);
            clientDto.OrderdId = client.OrderdId;   
            return clientDto;
        }
    }
}
