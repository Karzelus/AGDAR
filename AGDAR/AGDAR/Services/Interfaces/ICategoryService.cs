using AGDAR.Models.DTO;

namespace AGDAR.Services.Interfaces
{
    public interface ICategoryService
    {
        bool Update(int id, CategoryDto dto);
        bool Delete(int id);
        int Create(CategoryDto dto);
        List<CategoryDto> GetAll();
        CategoryDto GetById(int id);
    }
}