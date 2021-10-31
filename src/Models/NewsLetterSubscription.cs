using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Markel.com.Models
{
    public class NewsLetterSubscription
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
