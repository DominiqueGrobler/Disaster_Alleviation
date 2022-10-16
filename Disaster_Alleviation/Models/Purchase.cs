using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster_Alleviation.Models
{
    public class Purchase
    { 
        [Key]
        [Required(ErrorMessage = "Please enter your PurchaseID.")]
        public int PurchaseID { get; set; }
        public string Goods_Category{ get; set; }
        [Required(ErrorMessage = "Please enter number of items")]
        public int Num_items{ get; set; }
        [Required(ErrorMessage = "Please enter description of each item.")]
        public string Goods_Description { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter the date.")]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Please enter your purchase amount")]
        public int Amount { get; set; }
        public int DisasterID { get; internal set; }
        public string DisasterName { get; internal set; }
        public string Location { get; internal set; }
    }
}
