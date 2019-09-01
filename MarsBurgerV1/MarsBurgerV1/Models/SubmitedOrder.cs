using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class SubmitedOrder
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MMM yyyy")]
        public DateTime SubmitDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MMM yyyy")]
        public DateTime LastUpdate { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        public enum OrderStatus
        {
            OrderReceived,
            InPreperation,
            OnTheWay,
            Closed
        }
    }
}