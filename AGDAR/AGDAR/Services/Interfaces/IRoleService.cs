using AGDAR.Models;

namespace AGDAR.Services.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetAll();
        Role GetById(int id);
    }
}