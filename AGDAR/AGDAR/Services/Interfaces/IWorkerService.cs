using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IWorkerService
    {
        bool Update(int id, WorkerDto dto);
        bool Delete(int id);
        int Create(WorkerDto dto);
        List<WorkerDto> GetAll();
        WorkerDto GetById(int id);
    }
}