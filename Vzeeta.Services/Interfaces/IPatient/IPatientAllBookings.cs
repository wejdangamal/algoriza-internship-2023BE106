using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;

namespace Vzeeta.Services.Interfaces.IPatient
{
    public interface IPatientAllBookings
    {
        //[{image,doctorName,specialize,day,time,price,discoundCode,finalPrice,status}]
        public IEnumerable<PatientRequestDTO> GetAllBookings();
    }
}
