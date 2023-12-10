using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Core.ViewModels;

namespace Vzeeta.Services.Interfaces.IAdmin
{
    public interface IAdminSetting
    {
        Task<bool> Add(DiscountCodeVM entity);
        Task<bool> Update(DiscountCode_Coupon entity);
        Task<bool> Delete(int id);
        Task<bool> Deactivate(int Id);
    }
}
