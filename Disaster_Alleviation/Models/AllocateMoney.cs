using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class AllocateMoney
    {
        [Key]
        [Required(ErrorMessage = "Please enter your  ID.")]
        public int AllocateM { get; set; }
        [Required(ErrorMessage = "Please enter your donation amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Please enter the date.")]
        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        public string Donor { get; set; }
        public int DisasterID { get; internal set; }
        public string DisasterName { get; internal set; }
        public string Location { get; internal set; }
    }
}
