using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarkelServices.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Login> Get()
        {
            try
            {
                List<Login> x = new List<Login>();
                string line;

                string writefileName = "output.txt";
                string writepath = Path.Combine(Environment.CurrentDirectory, @"Files\", writefileName);

                // Read the file and display it line by line.
                using (StreamReader file = new StreamReader(writepath))
                {
                    //this will bypass the first line of heading (product, originyear,dev year, incremental value)
                    line = file.ReadLine();
                    while (line != null)
                    {
                        Login a = new Login();
                        //Read all lines after first and convert them into array of strings
                        a.Id = line.Split("-")[0];
                        a.Pwd = line.Split("-")[1];
                        x.Add(a);
                        line = file.ReadLine();
                    }

                    file.Close();
                }

                return x;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public bool SaveUserLogin(Login login)
        {
            // call to service which can use CRUD operations on database.
            //saving in a file for now

            string writefileName = "output.txt";
            string writepath = Path.Combine(Environment.CurrentDirectory, @"Files\", writefileName);
            using (StreamWriter file2 = new StreamWriter(writepath))
            {
                    file2.WriteLine(login.Id + "-" + login.Pwd);
                    file2.Close();
            }
            return true;
        }
    }
}
