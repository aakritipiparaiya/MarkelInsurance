
using Markel.com.Data.Interface;
using Markel.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Markel.com.Controllers
{
    /// <summary>
    /// Controller for Login Page
    /// </summary>
    [Route("/login")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepository _repository;
        private const string LoginViewName = "Login";
        private const string ErrorMsg = "Login Failed. Please contact Admin.";

        public LoginController(ILogger<LoginController> logger,
            ILoginRepository loginRepository)
        {
            _logger = logger;
            _repository = loginRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(LoginViewName);
        }

        /// <summary>
        /// This will check if Login Id and Pwd provided by the user are valid or not
        /// </summary>
        /// <param name="login">Login id and PWd</param>
        /// <returns>ActionResult</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Search(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if model is valid  check if the login id and pwd are valid
                    var isSuccess = await _repository.Search(login);

                    _logger.LogInformation("Search Login");
                    if (isSuccess)
                    {
                        //if login is valid change the text from "Login" to "Signout {id}"
                        //when user clicks on signout text will change back to Login for now
                        // can add functionality later on to actually signing user out and remove all its session data
                        ViewBag.Login = "Signout " + login.Id;
                        _logger.LogInformation("Valid login");

                        //after logging in - return back to main home page
                        return View("Views/Home/Index.cshtml");

                    }
                    else
                    {
                        ViewBag.result = ErrorMsg;
                        _logger.LogInformation("Login Failed");
                        return View(LoginViewName);
                    }
                }

                ViewBag.result = "Invalid State";
                return View(LoginViewName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                // instead of returning internal server error - can pass it on to global error handling page
                return StatusCode(500, "Internal server error:" + ex.Message);

            }
        }
    }
}
