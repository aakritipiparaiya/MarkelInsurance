using Markel.com.Data.Interface;
using Markel.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data.MockRepository
{
    /// <summary>
    /// A Mock Login Database Repository
    /// </summary>
    public class MockLoginRepository : ILoginRepository
    {
        private readonly HashSet<Login> userlogins = new HashSet<Login>();

        
        public MockLoginRepository()
        {
            //Mock Data
            userlogins.Add(new Login() { Id = "aakriti", Pwd = "india" });
            userlogins.Add(new Login() { Id = "markel", Pwd = "insurance" });
            userlogins.Add(new Login() { Id = "debby", Pwd = "orange" });
        }
        /// <summary>
        /// Add Logins in Login Table
        /// </summary>
        public Task<bool> Add(Login userlogin)
        {
            try
            {
                if (userlogin == null)
                    throw new ArgumentNullException(nameof(userlogin));

                if (string.IsNullOrWhiteSpace(userlogin.Id))
                    throw new ArgumentException("ID cant be null");

                if (string.IsNullOrWhiteSpace(userlogin.Pwd))
                    throw new ArgumentException("Pwd cant be null");

                userlogins.Add(userlogin);

                return Task.FromResult(true);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Search for Valid Login
        /// </summary>
        public Task<bool> Search(Login userlogin)
        {
            if (userlogin == null)
                throw new ArgumentNullException(nameof(userlogin));                      

            bool ifExist = userlogins.Any(c=>c.Id == userlogin.Id && c.Pwd == userlogin.Pwd);

            return Task.FromResult(ifExist);
        }
    }
}
