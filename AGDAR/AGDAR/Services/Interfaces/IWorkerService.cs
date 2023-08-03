using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IWorkerService
    {
        bool Update(int id, CreateWorkerDto dto);
        bool Delete(int id);
        int Create(CreateWorkerDto dto);
        List<WorkerDto> GetAll();
        WorkerDto GetById(int id);
    }
}