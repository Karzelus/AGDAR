using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Services.Interfaces;
using AGDAR.Services;

namespace AGDAR.Controllers
{
    public class PartsController : Controller
    {
        private readonly IPartService _partService;

        public PartsController(IPartService partService)
        {
            _partService = partService;
        }

        // GET: Parts
        public async Task<IActionResult> Index()
        {
            List<PartDto> parts = _partService.GetAll();
            if (parts != null)
            {
                return View(parts);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
        }

        //// GET: Parts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_partService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
            }

            var part = _partService.GetById(id);
            if (part == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }

            return View(part);
        }

        //// GET: Parts/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Parts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Type,ToolType")] PartDto part)
        {
            if (ModelState.IsValid)
            {
                _partService.Create(part);
                //await _workerService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        //// GET: Parts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_partService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
            }

            var part = _partService.GetById(id);
            if (part == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
            }
            return View(part);
        }

        //// POST: Parts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Type,ToolType")] PartDto part)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _partService.Update(id, part);
            if (!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
            }
            return View(part);
        }


        //// GET: Parts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_partService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
            }

            var part = _partService.GetById(id);
            if (part == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
            }

            return View(part);
        }

        //// POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _partService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.Parts'  is null.");
        }

        private bool PartExists(int id)
        {
            return (_partService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
