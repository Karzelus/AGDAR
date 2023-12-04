using AGDAR.Emails;
using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Repositories;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using System.Text;

namespace AGDAR.Controllers
{
    public class MailController : Controller
    {
        private static Random random = new Random();
        private readonly IPasswordHasher<Client> _passwordHasherClient;
        private readonly IEmailSender _emailSeneder;
        private readonly IClientService _clientService;
        private readonly NewsletterEmailsRepository _newsletterEmailRepository;
        private readonly ClientRepository _clientRepository;
        private readonly IProductService _productService;
        public MailController(IEmailSender emailSeneder, IClientService clientService, NewsletterEmailsRepository newsletterEmailRepository, IPasswordHasher<Client> passwordHasherClient, ClientRepository clientRepository, IProductService productService)
        {
            _emailSeneder = emailSeneder;
            _clientService = clientService;
            _newsletterEmailRepository = newsletterEmailRepository;
            _passwordHasherClient = passwordHasherClient;
            _clientRepository = clientRepository;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
             
            List<ClientDto> clients = _clientService.GetAll();
            return View(clients);
        }

        [HttpGet]
        public IActionResult SendCatalog()
        {
            var products = _productService.GetAll().ToList();
            var emails = _newsletterEmailRepository.GetAll().ToList();
            var subject = "Katalog Produktów";
            var message = "Katalog produktów na bierzący miesiąc: \n";
            int num = 1;
            foreach(var product in products)
            {
                if(product.Brand != "Custom")
                {
                    message +=num +". Nazwa: " + product.Name + " Cena: " + product.Price + "\n";
                    num++;
                }
            }
            foreach (var mail in emails)
            {
                _emailSeneder.SendEmailAsync(mail.Email, subject, message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ReleaseNewsletter()
        {
            var emails =_newsletterEmailRepository.GetAll().ToList();
            foreach(var mail in emails) 
            {
                var receiver = mail.Email;
                var subject = "Newsletter";
                var message = "Newsletter ";
                _emailSeneder.SendEmailAsync(receiver, subject, message);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddToNewsletterEmails(string email)
        {
            NewsletterEmails ne = new NewsletterEmails
            {
                Email = email,
            };
            _newsletterEmailRepository.AddAndSaveChanges(ne);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email)
        {
                     
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SendPassword(string email)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(chars.Length);
                password.Append(chars[index]);
            }
            string newPassword = password.ToString();

            var client = _clientService.GetByEmail(email);
            var hashedPassword = _passwordHasherClient.HashPassword(client, newPassword);
            client.Password = hashedPassword;
            _clientRepository.UpdateAndSaveChanges(client);

            var receiver = email;
            var subject = "Przywróć hasło";
            var message = "Twoje nowe hasło to " + newPassword + " możesz je zmienić w edycji swojego konta.";
            _emailSeneder.SendEmailAsync(receiver, subject, message);

            return View();
        }
        
    }
}
