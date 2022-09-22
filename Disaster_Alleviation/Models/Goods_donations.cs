using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Goods_donations
    {
        [Key]
        [Required(ErrorMessage = "Please enter your Donation ID.")]
        public int GoodsID { get; set; }
        public string Goods_Category{ get; set; }
        [Required(ErrorMessage = "Please enter number of items")]
        public int Num_items{ get; set; }
        [Required(ErrorMessage = "Please enter description of each item.")]
        public string Goods_Description { get; set; }

        [Required(ErrorMessage = "Please enter the date.")]
        public DateTime DonationDate { get; set; }

        public string Goods_Donor { get; set; }
    }
}
