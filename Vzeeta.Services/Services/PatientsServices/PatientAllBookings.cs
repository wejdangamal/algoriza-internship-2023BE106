using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IPatient;

namespace Vzeeta.Services.Services.PatientsServices
{
    public class PatientAllBookings : IPatientAllBookings
    {
        private readonly IRepository<Booking, int> patientBookings;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PatientAllBookings(IRepository<Booking, int> patientBookings, IHttpContextAccessor httpContextAccessor)
        {
            this.patientBookings = patientBookings;
            this.httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<PatientRequestDTO> GetAllBookings()
        {
            var patientId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allBookingsOgPatient = patientBookings.GetAllEntities()
                .Where(x => x.PatientId == patientId)
                .Select(bookingDetails => new PatientRequestDTO
                {
                    image = bookingDetails.TimeSlot.appointments.Doctor.ApplicationUsers.Image,
                    day = bookingDetails.TimeSlot.appointments.Day.ToString(),
                    doctorName = bookingDetails.TimeSlot.appointments.Doctor.ApplicationUsers.UserName,
                    finalPrice = bookingDetails.finalPrice,
                    discountCode = bookingDetails.DiscountCode,
                    price = bookingDetails.TimeSlot.appointments.Price,
                    time = bookingDetails.TimeSlot.time,
                    specialize = bookingDetails.specialization,
                    status = bookingDetails.status.ToString()
                }).ToList();

            return allBookingsOgPatient;
        }
    }
}
