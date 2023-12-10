using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.ViewModels;

namespace Vzeeta.Services.Interfaces.IDoctor
{
    public interface IDoctorRepository
    {
        Task<bool> Add(AppointmentsDTO model);
        Task<bool> Update(TimeDTO entity);
        Task<bool> Delete(int id);
        
    }
}
