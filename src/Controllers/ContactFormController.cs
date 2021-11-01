using Markel.com.Data;
using Markel.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Markel.com.Controllers
{
    /// <summary>
    /// This controller is for Contact Form page
    /// </summary>
    [Route("/contactUs")]
    public class ContactFormController : Controller
    {       
        private readonly ILogger<ContactFormController> _logger;
        private readonly IContactFormRepository _repository;
        private const string ContactUsViewName = "ContactForm";
        private const string ErrorMsg = "Error while registering your query. Please email us at markel@insurance.com.";
        private const string SuccessMsg = "Thank you for your Query. We will aim to resolve it within 2 - 3 days";

        public ContactFormController(ILogger<ContactFormController> logger,
            IContactFormRepository contactFormRepository)
        {
            _logger = logger;
            _repository = contactFormRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(ContactUsViewName);
        }

        /// <summary>
        /// This will add the Contact US query form in ContactForm Database
        /// </summary>
        /// <param name="contactForm">ContactForm Email, subject and description</param>
        /// <returns>ActionResult</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Add(ContactForm contactForm)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    //if there are no model errors, add the contact form into database
                    var isSuccess = await _repository.Add(contactForm);
                    //Check if its being added or not
                    ViewBag.result = isSuccess ? SuccessMsg : ErrorMsg; 
                    _logger.LogInformation("Contact us: valid state" + (isSuccess ? "success" : "failed"));

                }
                else
                {
                    ViewBag.result = ErrorMsg;
                    _logger.LogInformation("Contact us: invalid state");
                }

                ModelState.Clear();
                return View(ContactUsViewName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }
    }
}
