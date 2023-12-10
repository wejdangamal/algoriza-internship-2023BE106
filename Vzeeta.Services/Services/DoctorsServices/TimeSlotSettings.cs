using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IDoctorInterfaces;

namespace Vzeeta.Services.Services.DoctorsServices
{
    public class TimeSlotSettings : ITimeSlotSettings
    {
        private readonly IRepository<TimeSlot, int> _timeSlotrepository;
        private readonly IRepository<Booking, int> bookedTime;
        private readonly string currentDoctorId;
        public TimeSlotSettings(IRepository<TimeSlot, int> timeSlotRepository, IRepository<Booking, int> bookedTime,IHttpContextAccessor httpContextAccessor)
        {
            _timeSlotrepository = timeSlotRepository;
            this.bookedTime = bookedTime;
            currentDoctorId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<bool> Add(TimeSlot time)
        {
            if (time != null)
            {
                var res = await _timeSlotrepository.Add(time);
                return res;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var existTime = await _timeSlotrepository.GetById(id);
            var vaildDoctor = existTime?.appointments.doctorId == currentDoctorId;
            bool IsBooked = bookedTime.GetAllEntities().Any(x => x.timeId == id &&( x.status == Status.Pending||x.status==Status.Complete));
            if (existTime != null && vaildDoctor)
            {
                if (IsBooked)
                    throw new Exception("Time Is Booked Can't Delete it");
                else
                {
                    var res = await _timeSlotrepository.Delete(id);
                    return res;
                }

            }
            else
            {
                throw new Exception("Not Found!");
            }
        }

        public async Task<TimeSlot> GetById(int id)
        {
            var model = await _timeSlotrepository.GetById(id);
            return model;
        }

        public async Task<bool> Update(TimeSlot model)
        {
            var existTime = await _timeSlotrepository.GetById(model.timeId);
            var vaildDoctor = existTime?.appointments.doctorId == currentDoctorId;
            bool IsBooked = bookedTime.GetAllEntities().Any(x => x.timeId == model.timeId && (x.status == Status.Pending || x.status == Status.Complete));

            if (existTime != null && vaildDoctor)
            {
                if (IsBooked)
                    throw new Exception("Time Is Booked Can't Update it");
                else
                {
                    existTime.time = model.time;
                    var res = await _timeSlotrepository.Update(existTime);
                    return res;
                }

            }
            else
            {
                throw new Exception("Not Found!");
            }
        }
    }
}
/*
 * Object Of (Price & List Of Days(enum) Each Day have List Of Time) do not addtimes already exists
 * add the same day again with the same times => prevent
 * update the same day only and can add new times to it but take care of this times to not be already exist
 */