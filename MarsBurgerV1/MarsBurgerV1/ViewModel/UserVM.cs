using MarsBurgerV1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarsBurgerV1.ViewModel
{
    public class UserVM
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0: dd MM yyyy}")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public ICollection<AccountType> AccountTypes { get; set; }
        [Required]
        public int AccountTypesId { get; set; }
        public bool Disable { get; set; }

    }
}