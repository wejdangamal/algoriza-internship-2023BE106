using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Services.Interfaces.IPatient
{
    public interface ICancelBooking
    {
        Task<bool> CancelBookingAsync(int bookingId);
    }
}
