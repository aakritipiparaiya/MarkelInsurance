using Markel.com.Data;
using Markel.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Markel.com.Controllers
{
    /// <summary>
    /// Controller for NewsLetter Subscription page
    /// </summary>
    [Route("/subscribe")]
    public class SubscriptionController : Controller
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly INewsLetterSubscriptionRepository _repository;
        private const string SubscribeViewName = "NewsLetterSubscription";
        private const string ErrorMsg = "Error while registering your emailaddress. Please try later";
        private const string SuccessMsg = "You have successfully subscribed to our newsletter";
        private const string DuplicateMsg = "You have already been subscribed to our newsletter";

        public SubscriptionController(ILogger<SubscriptionController> logger,
            INewsLetterSubscriptionRepository newsLetterSubscriptionRepository)
        {
            _logger = logger;
            _repository = newsLetterSubscriptionRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(SubscribeViewName);
        }

        /// <summary>
        /// This is a post method which will add news letter subscription email to database
        /// </summary>
        /// <param name="sub">NewsLetterSubscription email</param>
        /// <returns>ActionResult</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Add(NewsLetterSubscription sub)
        {
            try
            {
                //check if model is valid
                if (ModelState.IsValid)
                {
                    //add the email add for subscription in newslettersubscription database
                    var isSuccess = await _repository.Add(new Models.NewsLetterSubscription { EmailAddress = sub.EmailAddress });
                    // if email was already present return duplicate msg else add in database
                    ViewBag.result = isSuccess ? SuccessMsg : DuplicateMsg;
                        _logger.LogInformation("NewsletterSubscription: " + (isSuccess ? SuccessMsg : DuplicateMsg));
                }
                else
                {
                    ViewBag.result = ErrorMsg;
                    _logger.LogInformation("NewsletterSubscription: " + ErrorMsg);
                }



                return View(SubscribeViewName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
