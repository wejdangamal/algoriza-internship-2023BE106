using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Core.ViewModels;

namespace Vzeeta.Services.Interfaces.IAdmin
{
    public interface IAdminDoctorBoard
    {
        IEnumerable<DocDTOS> GetAll(int page, int pageSize,Func<Doctor,bool> query, string[] includes = null);
        Task<DocDTOS> GetById(int Id);
        Task<bool> Update(DoctorUpdateDTO entity);
        Task<bool> Delete(int d);
       
    }
}
