using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface IProductService
    {
        bool Update(int id, CreateProductDto dto);
        public bool Delete(int id);
        int Create(CreateProductDto dto);
        List<ProductDto> GetAll();
        ProductDto GetById(int id);
    }
}