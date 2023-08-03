using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AGDAR.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository ) //Constructor
        {
            _roleRepository = roleRepository;
        }
        public Role GetById(int id)   //GetById
        {
            var role = _roleRepository.GetById(id);
            if (role is null)
            {
                return null;
            }
            return role;
        }

        public List<Role> GetAll() //GetAll
        {
            List<Role> roles = _roleRepository.GetAll().ToList();
            return roles;
        }
    }
}
