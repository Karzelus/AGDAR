using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGDAR.Models;
using AGDAR.Services.Interfaces;
using AGDAR.Services;
using AGDAR.Models.DTO;

namespace AGDAR.Controllers
{
    public class WorkersController : Controller
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            List<WorkerDto> workers = _workerService.GetAll();
            if (workers != null)
            {
                return View(workers);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_workerService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }

            var worker = _workerService.GetById(id);
            if (worker == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }

            return View(worker);
        }

        //// GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Workers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SeckondName,Email,DateOfBirth,Password,RoleId")] WorkerDto worker)
        {
            if (ModelState.IsValid)
            {
                _workerService.Create(worker);
                //await _workerService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }

        //// GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_workerService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }

            var worker = _workerService.GetById(id);
            if (worker == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }
            return View(worker);
        }

        //// POST: Workers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SeckondName,Email,DateOfBirth,Password,RoleId")] WorkerDto worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _workerService.Update(id, worker);
            if (!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }
            return View(worker);
        }

        //// GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_workerService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }

            var worker = _workerService.GetById(id);
            if (worker == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
            }

            return View(worker);
        }

        //// POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _workerService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.Workers'  is null.");
        }

        private bool WorkerExists(int id)
        {
            return (_workerService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
