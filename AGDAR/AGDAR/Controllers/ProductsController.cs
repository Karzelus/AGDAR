﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGDAR.Models;
using AGDAR.Services.Interfaces;
using AGDAR.Models.DTO;
using AGDAR.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AGDAR.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly OrderProductRepository _orderProductRepository;
        private readonly OrderRepository _orderRepository;

        public ProductsController(IProductService productService, ICategoryService categoryService, ProductCategoryRepository productCategoryRepository, OrderRepository orderRepository ,OrderProductRepository orderProductRepository )
        {
            _productService = productService;
            _categoryService = categoryService;
            _productCategoryRepository = productCategoryRepository;
            _orderProductRepository = orderProductRepository;
            _orderRepository = orderRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>();
            foreach (var c in _categoryService.GetAll().ToList())
            {
                categories.Add(new Category() { Name = c.Name });
            }

            ViewData["Kategoria"] = categories;
            List<ProductDto> products = _productService.GetAll();
            if(products != null)
            {
                return View(products);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_productService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
            }

            var product =  _productService.GetById(id);
            if (product == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
            }

            return View(product);
        }

        //// GET: Products/Create
        public IActionResult Create()
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            foreach(var c in _categoryService.GetAll().ToList())
            {
                categories.Add(new SelectListItem() { Text = c.Name, Value = c.Name });
            }
            ViewData["Kategoria"] = categories;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Brand,StateId,CategoriesId,Img")] CreateProductDto product)
        {

                _productService.Create(product);
                //await _productService.Save();
                return RedirectToAction(nameof(Index));

        }

        //// GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            foreach (var c in _categoryService.GetAll().ToList())
            {
                categories.Add(new SelectListItem() { Text = c.Name, Value = c.Name });
            }
            ViewData["Kategoria"] = categories;
            var product = _productService.GetById(id);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Brand,StateId,CategoriesId,Img")] ProductDto product)
        {
            var isUpdated = _productService.Update(id, product);
            if(!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_productService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
            }

            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult AddToCart(int productId, int orderId)
        {
            _productService.AddToCart(productId, orderId);
            var product = _productService.GetById(productId);
            var order = _orderRepository.GetById(orderId);
            order.Price += product.Price;
            _orderRepository.UpdateAndSaveChanges(order);

            _categoryService.GetById(orderId);
            return View();
        }
        [HttpGet]
        public IActionResult RemoveFromCart(int productId, int orderId)
        {
            _productService.RemoveFromCart(productId, orderId);
            var product = _productService.GetById(productId);
            var order = _orderRepository.GetById(orderId);
            order.Price -= product.Price;
            _orderRepository.UpdateAndSaveChanges(order);
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int productId, int orderId)
        //{
        //    return RedirectToAction(nameof(Index));
        //}

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _productService.Delete(id);
            if(isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
        }

        private bool ProductExists(int id)
        {
            return (_productService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
