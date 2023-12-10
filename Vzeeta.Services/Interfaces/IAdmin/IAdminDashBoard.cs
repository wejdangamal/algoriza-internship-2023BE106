using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;

namespace Vzeeta.Services.Interfaces.IAdmin
{
    public interface IAdminDashBoard
    {
        int NumOfDoctors();
        int NumOfPatients();
        IEnumerable<BookingDTO> NumOfRequests();
        IEnumerable<TopSpecializationDTO> TopFiveSpecializations();
        IEnumerable<TopDoctorsDTO> TopTenDoctors();

    }
}
