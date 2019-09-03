using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MMM yyyy")]
        public DateTime CreationTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MMM yyyy")]
        public DateTime LastUpdate { get; set; }
    }
}