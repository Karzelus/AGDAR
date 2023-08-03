using AGDAR.Models.DTO;
using AGDAR.Models;
using AutoMapper;
using AGDAR.Services.Interfaces;
using AGDAR.Repositories;

namespace AGDAR.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(CategoryRepository categoryRepository, IMapper mapper) //Construktor
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public bool Update(int id, CategoryDto dto) // Update
        {
            var category = _categoryRepository.GetById(id);
            if (category is null)
            {
                return false;
            }
            category.Name = dto.Name;
            _categoryRepository.UpdateAndSaveChanges(category);
            return true;
        }
        public bool Delete(int id) // Delete
        {
            var category = _categoryRepository.GetById(id);
            if (category is null)
            {
                return false;
            }
            _categoryRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }
        public CategoryDto GetById(int id) //GetById
        {
            var category = _categoryRepository.GetById(id);
            if (category is null)
            {
                return null;
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public List<CategoryDto> GetAll() //GetAll
        {
            var categories = _categoryRepository.GetAll().ToList();
            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoriesDtos;
        }

        public int Create(CategoryDto dto) //Create
        {
            var category = _mapper.Map<Category>(dto);
            _categoryRepository.AddAndSaveChanges(category);

            return category.Id;
        }
    }
}
