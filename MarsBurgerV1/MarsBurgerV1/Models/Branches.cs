using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.Models
{
    public class Branches
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string UserId { get; set; }
    }
}