using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Services.Interfaces
{
    public interface IBookingService
    {
       void calculateFinalPrice(DiscountCode_Coupon code, int userRequestsCount, ref decimal Price);
    }
}
