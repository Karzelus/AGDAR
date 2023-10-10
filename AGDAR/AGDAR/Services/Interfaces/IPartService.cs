using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IPartService
    {
        bool Update(int id,PartDto dto);
        bool Delete(int id);
        int Create(PartDto dto);
        List<PartDto> GetAll();
        PartDto GetById(int id);
    }
}