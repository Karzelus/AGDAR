using AGDAR.Models;
using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IClientService
    {
        bool Update(int id, ClientDto dto);
        bool ChangePassword(int id, ClientDto dto);
        bool UpdateClient(int id, Client dto);
        bool Delete(int id);
        int Create(ClientDto dto);
        List<ClientDto> GetAll();
        ClientDto GetById(int id);
        Client GetClientById(int id);
        Client GetByEmail(string email);
    }
}