using AGDAR.Models;
using AGDAR.Models.DTOs;
using Microsoft.Build.Framework;

namespace AGDAR.Services.Interfaces
{
    public interface IServiceProductService
    {
        bool Update(int id, ServiceProduct serviceProd);
        bool Delete(int id);
        ServiceProduct GetById(int id);
        List<ServiceProduct> GetAll();
        int Create(CreateServiceProductDto serviceProduct, int clientId);
    }
}
