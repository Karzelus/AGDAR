using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AGDAR.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(ProductRepository productRepository, IMapper mapper) //Constructor
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public bool Update(int id, CreateProductDto dto) // Update
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
            return productDto;
        }

        public List<ProductDto> GetAll() //GetAll
        {
            var products = _productRepository.GetAll().ToList();
            var productsDtos = _mapper.Map<List<ProductDto>>(products);
            return productsDtos;

        }

        public int Create(CreateProductDto dto) //Create
        {
            var product = _mapper.Map<Product>(dto);
            _productRepository.AddAndSaveChanges(product);

            return product.Id;
        }
    }
}

