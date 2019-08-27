using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class Addon
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}