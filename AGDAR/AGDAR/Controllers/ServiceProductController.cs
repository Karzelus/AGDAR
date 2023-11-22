using AGDAR.Models;
using AGDAR.Models.DTOs;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AGDAR.Models.Status;

namespace AGDAR.Controllers
{
    public class ServiceProductController : Controller
    {
        private readonly IServiceProductService _serviceProductService;
        private readonly IWorkerService _workerService;
        public ServiceProductController(IServiceProductService serviceProductService, IWorkerService workerService)
        {
            _serviceProductService = serviceProductService;
            _workerService = workerService;
        }

        // GET: ServiceProduct
        public async Task<IActionResult> Index()
        {
            List<ServiceProduct> serviceProduct = _serviceProductService.GetAll();
            if (serviceProduct != null)
            {
                Status statusService = new Status();
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
        //// POST: ServiceProduct/Edit/5
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

        public async Task<IActionResult> TakeReport(int id)
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
        //// POST: ServiceProduct/TakeReport/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeReport(int id,int workerId, [Bind("Id,ClientId,ClientEmail,Name,ClientNote,WorkerId,WorkerName,WorkerNote")] ServiceProduct serviceProduct)
        {
            var worker = _workerService.GetById(workerId);
            serviceProduct.WorkerName = worker.Name + " " + worker.SeckondName;
            serviceProduct.Status = 1;
            _serviceProductService.Update(id, serviceProduct);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Valuation(int id)
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
        //// POST: ServiceProduct/Valuation/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Valuation(int id, int workerId, [Bind("Id,ClientId,ClientEmail,Name,ClientNote,WorkerId,WorkerName,WorkerNote,Price")] ServiceProduct serviceProduct)
        {
            serviceProduct.Status = 2;
            _serviceProductService.Update(id, serviceProduct);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AcceptPrice(int id)
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
        //// POST: ServiceProduct/AcceptPrice/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptPrice(int id, int workerId, [Bind("Id,ClientId,ClientEmail,Name,ClientNote,WorkerId,WorkerName,WorkerNote,Price")] ServiceProduct serviceProduct)
        {
            serviceProduct.Status = 3;
            _serviceProductService.Update(id, serviceProduct);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finish(int id)
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
        //// POST: ServiceProduct/TakeReport/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int id, int workerId, [Bind("Id,ClientId,ClientEmail,Name,ClientNote,WorkerId,WorkerName,WorkerNote,Price,FinalNote")] ServiceProduct serviceProduct)
        {
            serviceProduct.Status = 4;
            _serviceProductService.Update(id, serviceProduct);

            return RedirectToAction(nameof(Index));
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
