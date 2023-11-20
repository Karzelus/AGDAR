using AGDAR.Models;
using AGDAR.Models.DTOs;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AGDAR.Controllers
{
    public class ServiceProductController : Controller
    {
        private readonly IServiceProductService _serviceProductService;
        public ServiceProductController(IServiceProductService serviceProductService)
        {
            _serviceProductService = serviceProductService;
        }

        // GET: ServiceProduct
        public async Task<IActionResult> Index()
        {
            List<ServiceProduct> serviceProduct = _serviceProductService.GetAll();
            if (serviceProduct != null)
            {
                return View(serviceProduct);
            }
            else
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
        }

        // GET: OrderHistory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_serviceProductService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }

            var serviceProduct = _serviceProductService.GetById(id);
            if (serviceProduct == null)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }

            return View(serviceProduct);
        }
        //// GET: ServiceProduct/Create
        public IActionResult Create()
        {
            return View();
        }
        //// POST: OrderHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ClientNote")] CreateServiceProductDto serviceProductDto, int clientId)
        {
            if (ModelState.IsValid)
            {
                _serviceProductService.Create(serviceProductDto, clientId);
                return RedirectToAction(nameof(Index));
            }
            return View(serviceProductDto);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (_serviceProductService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }

            var serviceProduct = _serviceProductService.GetById(id);
            if (serviceProduct == null)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }
            return View(serviceProduct);
        }

        //// POST: OrderHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ClientEmail,Name,ClientNote,WorkerId,WorkerName,WorkerNote")] ServiceProduct serviceProduct)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(ModelState);
            }

            var isUpdated = _serviceProductService.Update(id, serviceProduct);
            if (!isUpdated)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }
            return View(serviceProduct);
        }
        //// GET: OrderHistory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_serviceProductService.GetAll() == null)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }

            var serviceProduct = _serviceProductService.GetById(id);
            if (serviceProduct == null)
            {
                return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");
            }

            return View(serviceProduct);
        }

        //// POST: OrderHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = _serviceProductService.Delete(id);
            if (isDeleted) return RedirectToAction(nameof(Index));
            return NotFound("Entity set 'AGDARDbContext.ServiceProduct'  is null.");

        }

        private bool ServiceProductExists(int id)
        {
            return (_serviceProductService.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
