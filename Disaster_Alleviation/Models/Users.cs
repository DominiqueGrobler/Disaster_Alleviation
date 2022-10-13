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

        //this function Convert to Encord your Password
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }


    }
}
