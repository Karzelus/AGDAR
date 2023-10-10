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
using Microsoft.AspNetCore.Http;

namespace AGDAR.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            List<ClientDto> clients = _clientService.GetAll();
            if (clients != null)
            {
                return View(clients);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.Client'  is null.");
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_clientService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }

            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }

            return View(client);
        }

        //// GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SeckondName,Email,DateOfBirth,Password,OrderdId")] ClientDto client)
        {
            if (ModelState.IsValid)
            {
                _clientService.Create(client);
                //await _clientService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_clientService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }

            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }
            return View(client);
        }

        //// POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SeckondName,Email,DateOfBirth,Password,OrderdId")] ClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

            var isUpdated = _clientService.Update(id, client);
            if (!isUpdated)
            {
               return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }
            return View(client);
        }

        //// GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_clientService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }

            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");
            }

            return View(client);
        }

        //// POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _clientService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.Clients'  is null.");

        }

        private bool ClientExists(int id)
        {
            return (_clientService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
