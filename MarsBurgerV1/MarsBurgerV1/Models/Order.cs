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
        public string Name { get; set; }
        [Required]
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        [Required]
        public int AddonId { get; set; }
        public Addon Addon { get; set; }
        [Required]
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MMM yyyy")]
        public DateTime OrderDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd MMM yyyy")]
        public DateTime LastUpdate { get; set; }
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