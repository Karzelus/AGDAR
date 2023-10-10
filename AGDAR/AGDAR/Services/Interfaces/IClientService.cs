using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IClientService
    {
        bool Update(int id, ClientDto dto);
        bool Delete(int id);
        int Create(ClientDto dto);
        List<ClientDto> GetAll();
        ClientDto GetById(int id);
    }
}