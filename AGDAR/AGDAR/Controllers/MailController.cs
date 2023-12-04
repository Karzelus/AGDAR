using AGDAR.Emails;
using AGDAR.Models;
using AGDAR.Models.DTO;
using AGDAR.Repositories;
using AGDAR.Services;
using AGDAR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;

namespace AGDAR.Controllers
{
    public class MailController : Controller
    {
        private readonly IEmailSender _emailSeneder;
        private readonly IClientService _clientService;
        private readonly NewsletterEmailsRepository _newsletterEmailRepository;
        public MailController(IEmailSender emailSeneder, IClientService clientService, NewsletterEmailsRepository newsletterEmailRepository)
        {
            _emailSeneder = emailSeneder;
            _clientService = clientService;
            _newsletterEmailRepository = newsletterEmailRepository;
        }

        public async Task<IActionResult> Index()
        {
             
            //var receiver = "D.Gramacki@wp.pl";
            //var subject = "Test";
            //var message = "Hello World";

            //await _emailSeneder.SendEmailAsync(receiver, subject, message);
            List<ClientDto> clients = _clientService.GetAll();
            return View(clients);
        }

        [HttpGet]
        public IActionResult ReleaseNewsletter()
        {
            var emails =_newsletterEmailRepository.GetAll().ToList();
            foreach(var mail in emails) 
            {
                var receiver = mail.Email;
                var subject = "Newsletter";
                var message = "Hello World";
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
        public async Task<IActionResult> ForgotPassword()
        {

            return View();
        }

    }
}
