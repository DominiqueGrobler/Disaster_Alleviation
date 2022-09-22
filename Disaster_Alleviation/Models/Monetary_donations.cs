using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Monetary_donations
    {
        [Key]
        [Required(ErrorMessage = "Please enter your Monetary ID.")]
        public int MonetaryID { get; set; }
        [Required(ErrorMessage = "Please enter your donation amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Please enter the date.")]
        public DateTime DonationDate { get; set; }
        
        public string Donor{ get; set; }
    }
}
