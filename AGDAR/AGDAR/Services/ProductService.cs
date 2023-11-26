using AGDAR.Controllers;
using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Models.DTOs;
using AGDAR.Repositories;
using AGDAR.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace AGDAR.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly PartProductRepository _partProductRepository;
        private readonly OrderProductRepository _orderProductRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly PartRepository _partRepository;
        private readonly IMapper _mapper;
        public ProductService(ProductRepository productRepository, PartProductRepository partProductRepository, ProductCategoryRepository productCategoryRepository, PartRepository partRepository, CategoryRepository categoryRepository, OrderProductRepository orderProductRepository , IMapper mapper) //Constructor
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _categoryRepository = categoryRepository;
            _orderProductRepository = orderProductRepository;
            _partRepository = partRepository;
            _mapper = mapper;
            _partProductRepository = partProductRepository;
        }
        public bool Update(int id, ProductDto dto) // Update
        {
            var product = _productRepository.GetById(id);

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Brand = dto.Brand;
            product.StateId = dto.StateId;

            _productRepository.UpdateAndSaveChanges(product);

            var categories = _productCategoryRepository.GetAll()
                .Where(pc=>pc.ProductId == product.Id).ToList();

            foreach (var productCategory in categories)
            {
                _productCategoryRepository.Remove(productCategory);
            }

            _productCategoryRepository.SaveChanges();

            categories = _productCategoryRepository.GetAll()
                .Where(pc => pc.ProductId == product.Id).ToList();

            foreach (var name in dto.CategoriesId)
            {
                var category = _categoryRepository.Find(name);

                if (!categories.Any(cat => cat.CategoryId == category.Id))
                {
                    var productCategory = new ProductCategory()
                    {
                        ProductId = product.Id,
                        CategoryId = category.Id,
                    };
                    _productCategoryRepository.AddAndSaveChanges(productCategory);
                }
 
            }
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
            var productDto = _mapper.Map<ProductDto>(product);
            var productCategory = _productCategoryRepository.GetAll().Where(pc => pc.ProductId == id).ToList();
            foreach(var category in productCategory)
            {
                    productDto.Categories.Add(_categoryRepository.GetById(category.CategoryId));
            }
            return productDto;
        }

        public List<ProductDto> GetAll() //GetAll
        {       
            var products = _productRepository.GetAll().ToList();
            var productsDtos = _mapper.Map<List<ProductDto>>(products);
            foreach (var product in productsDtos)
            {
                product.Categories = getProductCategories(product.Id);
            }
            return productsDtos;

        }

        public List<Product> GetAllAdmin() //GetAll
        {
            var products = _productRepository.GetAll().ToList();
            return products;

        }
        private List<Category> getProductCategories(int Id)
        {
            var productCategories = _productCategoryRepository.GetAll().Where(pc => pc.ProductId == Id).ToList();
            List<Category> categories = new List<Category>();
            foreach ( var category in productCategories)
            {
                categories.Add(_categoryRepository.GetById(category.CategoryId));
            }
            return categories;
        }        
        public int CreateCustomProduct(CreateCustomProductDto dto) //Create
        {
            //var product = _mapper.Map<Product>(dto);
            var product = new Product()
            {
                Name = dto.Name,
                Price = 0,
                Description = dto.Description,
                Brand = "Custom",
                StateId = 0,
                Img = "Custom",
                Type = dto.Type,
            };

            _productRepository.AddAndSaveChanges(product);


            foreach (var name in dto.PartsId)
            {
                var part = _partRepository.Find(name);
                var partProduct = new PartProduct()
                {
                    ProductId = product.Id,
                    PartId = part.Id,
                };

                _partProductRepository.AddAndSaveChanges(partProduct);
            }
            return product.Id;
        }

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

        public void AddToCart(int productId, int orderId)
        {
            var orderProduct = new OrderProduct()
            {
                ProductId = productId,
                OrderId = orderId,
            };
            _orderProductRepository.AddAndSaveChanges(orderProduct);
        }

        public void RemoveFromCart(int productId, int orderId)
        {
            var cartProduct = _orderProductRepository
                .GetAll().First(op => op.ProductId == productId && op.OrderId == orderId);
            if(cartProduct != null) 
            { 
            _orderProductRepository.RemoveByIdAndSaveChanges(cartProduct.Id);
            
            }
        }

    }
}

