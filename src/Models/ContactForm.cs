using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Models
{
    public class ContactForm
    {        
        public int ID { get; set; } //this will be identity column

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [StringLength(50, ErrorMessage = "Subject can be max of 50 characters")]
        public string Subject { get; set; }

        [StringLength(400, ErrorMessage = "Description must be max of 400 characters")]
        public string Description { get; set; }
    }
}
