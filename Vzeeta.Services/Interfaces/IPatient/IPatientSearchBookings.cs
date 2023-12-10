using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;

namespace Vzeeta.Services.Interfaces.IPatient
{
    public interface IPatientSearchBookings
    {
        IEnumerable<AllDoctorsDetailsDTO> GetAll(int page, int pageSize, Func<Doctor, bool> query,string[] includes=null);
        Task<bool> Booking(int timeID, string? discountCode);
    }
}
