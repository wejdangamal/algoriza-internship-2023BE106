using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;

namespace Vzeeta.Services.Interfaces.IDoctorInterfaces
{
    public interface IDoctorRequests
    {
        Task<bool> confirmCheckUp(int bookingId);
        //List Of (PatientName/Image/Gender/Phone/Email/Appointments)
        List<DoctorPatientsRequestsDTO> GetAllRequests(int page,int pageSize,Func<Booking,bool> date, string[] includes=null);
    }
}
