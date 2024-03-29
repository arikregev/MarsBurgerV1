﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MarsBurgerV1.Utility.SD;

namespace MarsBurgerV1.Models
{
    public class Order
    {
        [Required]
        [Display(Name = "Order ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
        
        [Required]
        [Display(Name = "Creation Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }
        [Required]
        [Display(Name = "Last Update")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }
        [Required]
        public virtual int OrderStatusID
        {
            get
            {
                return (int)this.Status;
            }
            set
            {
                Status = (OrderStatus)value;
            }
        }
        [Required]
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }
        
    }
}