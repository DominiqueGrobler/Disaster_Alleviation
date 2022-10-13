using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Disaster
    {
        [Key]
        [Required(ErrorMessage = "Please enter your Disaster ID.")]
        public int DisasterID { get; set; }
        [Required(ErrorMessage = "Please enter your Disaster.")]
        public string DisasterName { get; set; }
        [Required(ErrorMessage = "Please enter your Location.")]
        public string Location{ get; set; }
        [Required(ErrorMessage = "Please enter your Description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter your Start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please enter your End date.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Please enter your Aid typr.")]
        public string AidType { get; set; }
        public string Status { get;set; }


    }
}
