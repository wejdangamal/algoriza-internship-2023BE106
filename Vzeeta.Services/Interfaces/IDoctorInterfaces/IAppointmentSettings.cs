using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;

namespace Vzeeta.Services.Interfaces.IDoctorInterfaces
{
    public interface IAppointmentSettings
    {
        Task<bool> Add(Appointments appointments);
        Task<Appointments> GetById(int id);
    }
}
