using Markel.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    public sealed class ContactFormRepository : IContactFormRepository
    {
        private readonly MarkelDbContext _dbContext;

        public ContactFormRepository(MarkelDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        /// Add Contact Form in Database
        /// </summary>
        public async Task<bool> Add(ContactForm contactform)
        {
            try
            {
                if (contactform != null)
                {
                    await _dbContext.ContactForms.AddAsync(contactform);
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
        /// Get All Contact Forms
        /// </summary>
        public Task<IEnumerable<ContactForm>> GetAllForms()
        {
            try
            {
                return Task.FromResult(_dbContext.ContactForms.AsEnumerable<ContactForm>());
            }
            catch
            {
                throw;
            }
        }
    }
}
