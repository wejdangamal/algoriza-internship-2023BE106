using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.DTOs
{
    public class DoctorPatientsRequestsDTO
    {
            public PatientsDTO details { get; set; }
            public List<PatientAppointmentsDTO> appointments { get; set; }
    }
    public class PatientAppointmentsDTO
    {
        public string day { get; set; }
        public TimeSpan time { get; set; }
    }
}
