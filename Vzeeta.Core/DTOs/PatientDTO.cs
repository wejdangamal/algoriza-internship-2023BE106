using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.DTOs
{
    //[details:{image(if found), fullName, email, phone, gender, dateOfBirth}
    public class PatientDTO
    {
        public PatientsDTO details { get; set; }
        public List<PatientRequestDTO> requests { get; set; }
    }
}
