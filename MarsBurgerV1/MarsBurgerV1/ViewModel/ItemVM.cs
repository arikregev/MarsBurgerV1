﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MarsBurgerV1.Utility.SD;

namespace MarsBurgerV1.ViewModel
{
    public class ItemVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public ItemType Type {get; set;}
        public string ImageURL { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}