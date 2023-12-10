using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.ViewModels;

namespace Vzeeta.Core.DTOs
{
    public class DoctorUpdateDTO
    {
        [Required]
        public int id { get; set; }
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
        [Required]
        public string email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invaild Password")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Please enter a valid phone number.")]
        public string phone { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int specializeId { get; set; }
    }
    public class DocDTOS
    {
       
        public string fullName { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string specialize { get; set; }
        public DateTime dateOfBirth { get; set; }
        public byte[] image { get; set; }
    }
}
