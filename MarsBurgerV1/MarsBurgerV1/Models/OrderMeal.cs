using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class OrderMeal
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MealId { get; set; }
        [Required]
        public virtual ICollection<Addon> Addons { get; set; }
    }
}