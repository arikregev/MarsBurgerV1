using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class AccountType
    {
        [Required]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }

    }
}