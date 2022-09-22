using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Users
    {

        [Key]
        [Required(ErrorMessage = "Please enter your Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your Password.")]
        public string Password { get; set; }


    }
}
