using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGDAR.Models;
using AGDAR.Services.Interfaces;
using AGDAR.Models.DTO;
using AGDAR.Services;

namespace AGDAR.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            List<CategoryDto> categories = _categoryService.GetAll();
            if (categories != null)
            {
                return View(categories);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_categoryService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }

            return View(category);
        }

        //// GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(category);
                //await _categoryService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //// GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_categoryService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }
            return View(category);
        }

        //// POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _categoryService.Update(id, category);
            if(!isUpdated) 
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }
            return Redirect("/Categories");
        }

        //// GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_categoryService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
            }

            return View(category);
        }

        //// POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _categoryService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.Categories'  is null.");
        }

        private bool categoryexists(int id)
        {
            return (_categoryService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
