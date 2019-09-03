using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MarsBurgerV1.Utility.SD;

namespace MarsBurgerV1.Models
{
    public class OrderItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public virtual int ItemTypeId { get; set; }
        [EnumDataType(typeof(ItemType))]
        public ItemType ItemID { get; set; }
        
    }
}