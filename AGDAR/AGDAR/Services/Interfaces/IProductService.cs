using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Models.DTOs;

namespace AGDAR.Services.Interfaces
{
    public interface IProductService
    {
        bool Update(int id, ProductDto dto);
        bool Valuate(int id, CreateCustomProductDto dto);
        public bool Delete(int id);
        int Create(CreateProductDto dto);
        int CreateCustomProduct(CreateCustomProductDto dto);
        List<ProductDto> GetAll();
        List<Product> GetAllAdmin();
        ProductDto GetById(int id);
        CreateCustomProductDto GetCustomById(int id);
        void AddToCart(int productId,int orderId);
        void RemoveFromCart(int productId,int orderId);
        
    }
}