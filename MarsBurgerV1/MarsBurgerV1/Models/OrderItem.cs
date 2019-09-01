using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class OrderItem
    {
        [Required]
        public int Id { get; set; }
        public virtual ICollection<OrderMeal> OrderMeals { get; set; }
        public virtual ICollection<Meal> Drinks { get; set; }
        public virtual ICollection<Meal> SideDishes { get; set; }
        

    }
}