using Markel.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Data
{
    public interface IContactFormRepository
    {
        Task<bool> Add(ContactForm contactForm);
        Task<IEnumerable<ContactForm>> GetAllForms();
    }
}
