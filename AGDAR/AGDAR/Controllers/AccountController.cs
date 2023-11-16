using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using AGDAR.Models.DTOs;
using Microsoft.AspNetCore.Http;
using AGDAR.Repositories;

namespace AGDAR.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OrderRepository _orderRepositry;

        public AccountController(IAccountService accountService, IHttpContextAccessor httpContextAccessor, OrderRepository orderRepository)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _orderRepositry = orderRepository;
        }
        // GET: Account/CreateClient
        public IActionResult CreateClient()
        {
            return View();
        }
        // POST: Account/CreateClient.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateClient([Bind("Id,Name,SeckondName,Email,DateOfBirth,Password,ConfirmPassword,OrderId,Order")] ClientDto clientDto)
        public async Task<IActionResult> CreateClient([Bind("Id,Email,Password,ConfirmPassword,Name,SeckondName,DateOfBirth")] ClientDto clientDto)
        {
            if (ModelState.IsValid)
            {
                _accountService.RegisterClient(clientDto);
                return Redirect("/Products");
            }
            return View(clientDto);
        }
        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Account/Create.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SeckondName,Email,DateOfBirth,Password,ConfirmPassword,RoleId,Role")] WorkerDto workerDto)
        {
            if (ModelState.IsValid)
            {
                _accountService.RegisterWorker(workerDto);
                //await _workerService.Save();
                return Redirect("/Products");
            }
            return View(workerDto);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginDto dto)
        {         
            //string token = _accountService.GenerateJwt(dto);
            string token = _accountService.GenerateJwt(HttpContext, dto);
            ViewBag.JwtToken = token;
            var worker = _accountService.GetWorkerByEmail(dto.Email); // Załóżmy, że masz tę metodę w IAccountService
            HttpContext.Session.SetString("WorkerEmail", worker.Email);
            HttpContext.Session.SetString("WorkerId", worker.Id.ToString());
            return Redirect("/Products");
        }

        public IActionResult LoginClient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginClient([Bind("Email,Password")] LoginDto dto)
        {
            //string token = _accountService.GenerateJwt(dto);
            string token = _accountService.GenerateClientJwt(HttpContext, dto);
            ViewBag.JwtToken = token;
            var client = _accountService.GetClientByEmail(dto.Email); // Załóżmy, że masz tę metodę w IAccountService
            HttpContext.Session.SetString("ClientEmail", client.Email);
            HttpContext.Session.SetString("ClientName", client.Name);
            HttpContext.Session.SetString("ClientOrderId", client.OrderdId.ToString());
            HttpContext.Session.SetString("ClientId", client.Id.ToString());

            return Redirect("/Products");
        }

        public async Task<IActionResult> Logout()
        {
            if (HttpContext.Session.GetString("ClientId") != null)
            {
                HttpContext.Session.Remove("ClientId");
                HttpContext.Session.Remove("ClientEmail");
            }
            if (HttpContext.Session.GetString("WorkerId") != null)
            {
                HttpContext.Session.Remove("WorkerId");
                HttpContext.Session.Remove("WorkerEmail");
            }
            return Redirect("/Products");
        }
    }
}
