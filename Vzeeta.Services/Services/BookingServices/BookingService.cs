using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Services.Interfaces;

namespace Vzeeta.Services.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        public void calculateFinalPrice(DiscountCode_Coupon code, int userRequestsCount, ref decimal Price)
        {
            if (userRequestsCount % code.NoOfRequests == 0)
            {
                if (code.DiscountType == DiscountType.Percentage)
                {
                    Price = Price < code.Value ? 0 : Price - Price * (code.Value / 100);
                }
                else
                {
                    Price = Price < code.Value ? 0 : Price - code.Value;
                }

            }
            else
            {
                throw new Exception($"Invalid Number Of Completed Requests to use code");
            }
        }
    }
}
