using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;

namespace Vzeeta.Services.Interfaces.IDoctorInterfaces
{
    public interface ITimeSlotSettings
    {
        Task<bool> Add(TimeSlot time);
        Task<bool> Update(TimeSlot time);
        Task<bool> Delete(int id);
        Task<TimeSlot> GetById(int id);
    }
}
