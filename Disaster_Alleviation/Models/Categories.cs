using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Categories
    {
        [Key]
        [Required(ErrorMessage = "Please enter your Disaster ID.")]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Please enter your Disaster.")]
        public string Category_Name { get; set; }
    }
}
