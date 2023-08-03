using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IPartService
    {
        bool Update(int id, CreatePartDto dto);
        bool Delete(int id);
        int Create(CreatePartDto dto);
        IEnumerable<PartDto> GetAll();
        PartDto GetById(int id);
    }
}