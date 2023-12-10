using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IPatient;

namespace Vzeeta.Services.Services.PatientsServices
{
    public class CancelBooking : ICancelBooking
    {
        private readonly IRepository<Booking, int> bookingContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CancelBooking(IRepository<Booking, int> bookingContext, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingContext = bookingContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var currentUserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var findRequest = await bookingContext.GetById(bookingId);
            if (findRequest != null)
            {
                if (findRequest.status == Status.Complete)
                {
                    throw new Exception("Invalid Booking Cancellation ,Already Completed");
                }
                else
                {
                    if(findRequest.PatientId == currentUserId)
                    {
                        findRequest.status = Status.Cancelled;
                        var updateBooking = await bookingContext.Update(findRequest);
                        if (updateBooking)
                        {
                            return true;
                        }
                    }     
                }          
            }
            throw new Exception("Invalid Booking Cancellation");
        }
    }
}
