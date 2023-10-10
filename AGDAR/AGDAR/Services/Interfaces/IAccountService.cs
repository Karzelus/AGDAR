using AGDAR.Models.DTO;
using AGDAR.Models.DTOs;

namespace AGDAR.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterWorker(WorkerDto dto);
        void RegisterClient(ClientDto dto);
        string GenerateJwt(HttpContext httpContext, LoginDto dto);
        string GenerateClientJwt(HttpContext httpContext, LoginDto dto);
        WorkerDto GetWorkerByEmail(string email);
        ClientDto GetClientByEmail(string email);
        //string GenerateJwt(LoginDto dto);
    }
}
