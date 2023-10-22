using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AGDAR.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProductService(ProductRepository productRepository, ProductCategoryRepository productCategoryRepository, CategoryRepository categoryRepository, IMapper mapper) //Constructor
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public bool Update(int id, ProductDto dto) // Update
        {
            var product = _productRepository.GetById(id);
            if (product is null)
            {
                return false;
            }
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Brand = dto.Brand;
            product.StateId = dto.StateId;

            _productRepository.UpdateAndSaveChanges(product);
            return true;
        }
        public bool Delete(int id) // Delete
        {
            var product = _productRepository.GetById(id);
            if (product is null)
            {
                return false;
            }
            _productRepository.RemoveByIdAndSaveChanges(id);
            return true;
        }
        public ProductDto GetById(int id)   //GetById
        {
            var product = _productRepository.GetById(id);
            if (product is null)
            {
                return null;
            }
            var productDto = _mapper.Map<ProductDto>(product);
            var productCategory = _productCategoryRepository.GetAll().Where(pc => pc.ProductId == id).ToList();
            foreach(var category in productCategory)
            {
                    productDto.CategoriesList.Add(_categoryRepository.GetById(category.CategoryId));
            }
            return productDto;
        }

        public List<ProductDto> GetAll() //GetAll
        {
            var products = _productRepository.GetAll().ToList();
            var productsDtos = _mapper.Map<List<ProductDto>>(products);
            return productsDtos;

        }
        //private List<ProductCategory> GetProductCategories(int id)
        //{
        //    List<ProductCategory> categoriesList = _productCategoryRepository.GetAll().ToList();
        //    var categories = "";
        //    return categories;
        //}

        public int Create(CreateProductDto dto) //Create
        {
            var product = _mapper.Map<Product>(dto);

            
            _productRepository.AddAndSaveChanges(product);
            

            foreach(var name in dto.CategoriesId)
            {
                var category = _categoryRepository.Find(name);

                if(category is null)
                {
                    category = new Category()
                    {
                        Name = name,
                    };

                    _categoryRepository.AddAndSaveChanges(category);
                }

                var productCategory = new ProductCategory()
                {
                    ProductId = product.Id,
                    CategoryId = category.Id,
                };

                _productCategoryRepository.AddAndSaveChanges(productCategory);
            }
            return product.Id;
        }
    }
}

