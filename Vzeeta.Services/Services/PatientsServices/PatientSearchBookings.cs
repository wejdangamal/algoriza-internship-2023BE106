using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IPatient;

namespace Vzeeta.Services.Services.PatientsServices
{
    public class PatientSearchBookings : IPatientSearchBookings
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRepository<Booking, int> request;
        private readonly IRepository<DiscountCode_Coupon, int> codeCoupon;
        private readonly Core.Repository.IRepository<Doctor, int> doctorsDetails;
        private readonly IRepository<TimeSlot, int> timeContext;

        public PatientSearchBookings(IHttpContextAccessor httpContextAccessor, IRepository<Booking, int> request,
            IRepository<DiscountCode_Coupon, int> codeCoupon, IRepository<Doctor, int> doctorsDetails, IRepository<TimeSlot, int> timeContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.request = request;
            this.codeCoupon = codeCoupon;
            this.doctorsDetails = doctorsDetails;
            this.timeContext = timeContext;
        }
        public async Task<bool> Booking(int timeID, string? discountCode)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existsId = await timeContext.GetById(timeID);
            if (existsId != null)
            {
                var bookedTime = request.GetAllEntities().Any(x => x.timeId == timeID && x.status == Status.Pending);
                var getTimeDoctorId = timeContext.GetAllEntities().Where(x => x.timeId == timeID).Select(x => x.appointments.doctorId).FirstOrDefault();
                var bookedWithTheSameDoctor = request.GetAllEntities().Any(x => x.PatientId == userId && x.TimeSlot.appointments.Doctor.ApplicationUserId == getTimeDoctorId && x.status == Status.Pending);
                var userCompletedRequestsCount = request.GetAllEntities().Where(x => x.PatientId == userId && x.status == Status.Complete).Count();
                if (!bookedWithTheSameDoctor)
                {
                    if (!bookedTime)
                    {
                        Booking newRequest = new Booking
                        {
                            PatientId = userId,
                            timeId = timeID,
                            price = existsId.appointments.Price,
                            specialization = existsId.appointments.Doctor.specializations.specializeType,
                            finalPrice = existsId.appointments.Price
                        };
                        var finalPrice = existsId.appointments.Price;
                        if (discountCode != null)
                        {
                            var vaildCode = codeCoupon.GetAllEntities().SingleOrDefault(x => x.Code == discountCode && !x.IsExpired);
                            if (vaildCode != null)
                            {
                                var IsUsedOnce = request.GetAllEntities().Any(x => x.PatientId == userId && x.DiscountCode == discountCode);
                                if (!IsUsedOnce)
                                {
                                    newRequest.DiscountCode = discountCode;
                                    calculateFinalPrice(vaildCode.DiscountType, userCompletedRequestsCount, vaildCode.NoOfRequests, vaildCode.Value, ref finalPrice);
                                    newRequest.finalPrice = finalPrice;
                                }
                                else
                                {
                                    throw new Exception("This Discount Code Used Before");
                                }
                            }
                            else
                            {
                                throw new Exception("Invaild DiscountCode Coupon!");
                            }
                        }
                        var result = await request.Add(newRequest);
                        return result;
                    }
                }
                else
                {
                    throw new Exception("Invaild Booking");
                }
            }
            throw new Exception("Invalid Time");
        }
        //[{image,fullName,email,phone,specialize,price,gender,appointments:[{day,times:[{id,time}]}]}]
        public IEnumerable<AllDoctorsDetailsDTO> GetAll(int page, int pageSize, Func<Doctor, bool> query, string[] includes = null)
        {
            var allDoctors = doctorsDetails.GetAll(page, pageSize, query, includes);
            var allDoctorsDetails = allDoctors.Select(doctor => new AllDoctorsDetailsDTO
            {
                details = new DocDTOS
                {
                    fullName = doctor.ApplicationUsers.UserName,
                    email = doctor.ApplicationUsers.Email,
                    dateOfBirth = doctor.ApplicationUsers.DateOfBirth,
                    image = doctor.ApplicationUsers.Image,
                    phone = doctor.ApplicationUsers.PhoneNumber,
                    specialize = doctor.specializations.specializeType,
                    gender = doctor.ApplicationUsers.gender.ToString()
                },
                price = doctor.Appointments.Select(x => x.Price).FirstOrDefault(),
                appointments = doctor.Appointments.Select(appointment => new AppointmentsNDTO
                {
                    day = appointment.Day.ToString(),
                    times = appointment.times.Select(a => new TimeSpanDTO
                    {
                        id = a.timeId,
                        time = a.time
                    }).ToList()
                }).ToList()
            }).ToList();
            return allDoctorsDetails;
        }
        private void calculateFinalPrice(DiscountType codeType, int userRequestsCount, int numOfReqs, decimal value, ref decimal Price)
        {
            if (numOfReqs == 0 || userRequestsCount % numOfReqs == 0)
            {
                if (codeType == DiscountType.Percentage)
                {
                    Price = Price < value ? 0 : Price - Price * (value / 100);
                }
                else
                {
                    Price = Price < value ? 0 : Price - value;
                }

            }
            throw new Exception($"Invalid Number Of Completed Requests to use code");

        }
    }
}
