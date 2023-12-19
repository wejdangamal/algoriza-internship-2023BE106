using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.ViewModels
{
    public class SignInDTO
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invaild Email")]
        [RegularExpression(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",ErrorMessage ="Invaild email Pattern")]
        public string email { get; set;}
        [Required]
        [DataType(DataType.Password,ErrorMessage ="Invaild Password")]
        public string password { get; set;}
    }
}
