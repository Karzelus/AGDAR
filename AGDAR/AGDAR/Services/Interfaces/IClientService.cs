using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IClientService
    {
        bool Update(int id, CreateClientDto dto);
        bool Delete(int id);
        int Create(CreateClientDto dto);
        List<ClientDto> GetAll();
        ClientDto GetById(int id);
    }
}