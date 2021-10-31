using Markel.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    /// <summary>
    /// A Mock Contact Form Database
    /// </summary>
    public class MockContactFormRepository : IContactFormRepository
    {
        private readonly HashSet<ContactForm> forms = new HashSet<ContactForm>();
        
        /// <summary>
        ///Add Contact Form in Mock Database
        /// </summary>
        public Task<bool> Add(ContactForm contactForm)
        {

            try
            {
                if (contactForm == null)
                    throw new ArgumentNullException(nameof(contactForm));

                if (string.IsNullOrWhiteSpace(contactForm.EmailAddress))
                    throw new ArgumentException("Email Address is empty");

                if (!ValidateEmail(contactForm.EmailAddress))
                    throw new Exception("Email not valid");

                forms.Add(contactForm);

                return Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Validate if Email is in correct format or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success) return true;
            return false;
        }


        /// <summary>
        /// Get All Contact Forms
        /// </summary>
        public Task<IEnumerable<ContactForm>> GetAllForms()
        {
            try
            {
                return Task.FromResult(forms.AsEnumerable<ContactForm>());
            }
            catch
            {
                throw;
            }
        }
    }
}
