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

namespace AGDAR.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
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
            var categories = _categoryService.GetAll().ToList(); // Wywołanie metody, która pobiera dostępne kategorie
            ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Brand,StateId,Categories,Img")] CreateProductDto product)
        {

                _productService.Create(product);
                //await _productService.Save();
                return RedirectToAction(nameof(Index));

        }

        //// GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
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

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Brand,StateId,Categories")] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _productService.Update(id, product);
            if(!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.Products'  is null.");
            }
            return View(product);
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
