using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Markel.com.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Id is required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Pwd { get; set; }
    }
}
