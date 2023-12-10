using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.DTOs
{
    public class BaseRegistrationDTO
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public Gender gender { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invaild Date")]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Invaild Email")]
        [RegularExpression(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invaild email Pattern please@")]
        [Required]
        public string email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invaild PhoneNumber")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Please enter a valid phone number.")]
        public string phone { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Invaild Password")]
        public string password { get; set; }
    }
}
