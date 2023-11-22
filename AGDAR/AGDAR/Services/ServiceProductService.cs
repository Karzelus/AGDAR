using AGDAR.Models;
using AGDAR.Models.DTOs;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;

namespace AGDAR.Services
{
    public class ServiceProductService : IServiceProductService
    {
        private readonly ServiceProductRepository _serviceProductRepository;
        private readonly ClientRepository _clientRepository;

        public ServiceProductService(ServiceProductRepository serviceProductRepository, ClientRepository clientRepository)
        {
            _serviceProductRepository = serviceProductRepository;
            _clientRepository = clientRepository;
        }

        public bool Update(int id, ServiceProduct serviceProd) // Update
        {
            var serviceProductToUpdate = _serviceProductRepository.GetById(id);
            if (serviceProductToUpdate is null)
            {
                return false;
            }
            serviceProductToUpdate.ClientId = serviceProd.ClientId;
            serviceProductToUpdate.Name = serviceProd.Name;
            serviceProductToUpdate.ClientNote = serviceProd.ClientNote;
            serviceProductToUpdate.ClientEmail = serviceProd.ClientEmail;
            serviceProductToUpdate.WorkerId = serviceProd.WorkerId;
            serviceProductToUpdate.WorkerName = serviceProd.WorkerName;
            serviceProductToUpdate.WorkerNote = serviceProd.WorkerNote;
            serviceProductToUpdate.Status = serviceProd.Status;
            serviceProductToUpdate.Price = serviceProd.Price;
            serviceProductToUpdate.FinalNote = serviceProd.FinalNote;

            _serviceProductRepository.UpdateAndSaveChanges(serviceProductToUpdate);
            return true;
        }

        public bool Delete(int id) // Delete
        {
            var serviceProductToDelete = _serviceProductRepository.GetById(id);
            if (serviceProductToDelete is null)
            {
                return false;
            }
            _serviceProductRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }

        public ServiceProduct GetById(int id)   //GetById
        {
            var serviceProduct = _serviceProductRepository.GetById(id);
            if (serviceProduct is null)
            {
                return null;
            }
            return serviceProduct;
        }

        public List<ServiceProduct> GetAll() //GetAll
        {
            var serviceProducts = _serviceProductRepository.GetAll().ToList();
            return serviceProducts;
        }

        public int Create(CreateServiceProductDto serviceProduct,int clientId) //Create
        {
            var client = _clientRepository.GetById(clientId);
            ServiceProduct newServiceProduct = new ServiceProduct();
            newServiceProduct.Id = serviceProduct.Id;
            newServiceProduct.Name = serviceProduct.Name;
            newServiceProduct.ClientNote = serviceProduct.ClientNote;
            newServiceProduct.ClientId = client.Id;
            newServiceProduct.ClientEmail = client.Email;
            newServiceProduct.Status = 0;
            _serviceProductRepository.AddAndSaveChanges(newServiceProduct);


            return serviceProduct.Id;
        }
    }
}
