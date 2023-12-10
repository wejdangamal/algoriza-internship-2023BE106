using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;

namespace Vzeeta.Core.DTOs
{
    //[{image(if found),fullName,email,phone,gender,dateOfBirth}]
    public class PatientsDTO
    {
        public byte[]? image { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
