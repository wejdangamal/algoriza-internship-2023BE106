using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;

namespace Vzeeta.Services.Interfaces.IAdmin
{
    public interface IAdminPatientsBoard
    {      
        IEnumerable<PatientsDTO> GetAll(int page, int pageSize, Func<ApplicationUser, bool> query);
        Task<PatientDTO> GetById(string id);
    }
}
