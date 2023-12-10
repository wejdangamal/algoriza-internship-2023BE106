using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IDoctorInterfaces;

namespace Vzeeta.Services.Services.DoctorsServices
{
    public class DoctorRequests : IDoctorRequests
    {
        private readonly IRepository<Booking, int> bookingContext;

        public DoctorRequests(IRepository<Booking, int> bookingContext)
        {
            this.bookingContext = bookingContext;
        }
        public async Task<bool> confirmCheckUp(int bookingId)
        {
            var findRequest = await bookingContext.GetById(bookingId);
            if (findRequest != null)
            {
                if(findRequest.status==Status.Pending)
                {
                    findRequest.status = Status.Complete;
                    var updateBooking = await bookingContext.Update(findRequest);
                    if (updateBooking)
                    {
                        return true;
                    }
                }
            }
            throw new Exception("Invalid Booking Confirmation");
        }

        public  List<DoctorPatientsRequestsDTO> GetAllRequests(int page, int pageSize, Func<Booking, bool> date, string[] includes=null)
        {
            var requests = bookingContext.GetAll(page, pageSize, date, includes).ToList();
            List<DoctorPatientsRequestsDTO> requestsList = new List<DoctorPatientsRequestsDTO>();
            foreach (var request in requests)
            {
                requestsList.Add(
                    new DoctorPatientsRequestsDTO
                    {
                        details = new PatientsDTO
                        {
                            fullName = request.Patient.UserName,
                            dateOfBirth = request.Patient.DateOfBirth,
                            email = request.Patient.Email,
                            gender = request.Patient.gender.ToString(),
                            image = request.Patient.Image,
                            phone = request.Patient.PhoneNumber
                        },
                        appointments = new List<PatientAppointmentsDTO>
                        {
                            new PatientAppointmentsDTO
                            {
                                day = request.TimeSlot.appointments.Day.ToString(),
                                time = request.TimeSlot.time
                            }
                        }

                    });
                }
            return requestsList;
        }
    }
}
