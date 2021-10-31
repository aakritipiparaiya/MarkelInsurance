using Markel.com.Data.Interface;
using Markel.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data.DBRepository
{
    public sealed class LoginRespository : ILoginRepository
    {
        private readonly MarkelDbContext _dbContext;

        public LoginRespository(MarkelDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        /// Add All Login Ids and Pwd in database
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<bool> Add(Login login)
        {
            try
            {
                if (login != null)
                {
                    await _dbContext.Login.AddAsync(login);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Search if the passed Login Id and Password is valid or not
        /// </summary>

        public async Task<bool> Search(Login login)
        {
            try
            {
                if (login != null)
                {
                    await _dbContext.Login.FindAsync(login.Id);
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

    }
}
