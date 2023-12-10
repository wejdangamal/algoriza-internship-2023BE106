using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Data;
using Vzeeta.Repository;
using Vzeeta.Services.Interfaces.IDoctor;
using Vzeeta.Services.Interfaces.IDoctorInterfaces;

namespace Vzeeta.Services.Services.DoctorsServices
{
    public class DoctorAppointmentsService : IDoctorRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAppointmentSettings _appointmentSettings;
        private readonly ITimeSlotSettings _timeSlotSettings;

        public DoctorAppointmentsService(IHttpContextAccessor httpContextAccessor, IAppointmentSettings appointmentSettings, ITimeSlotSettings timeSlotSettings)
        {
            this.httpContextAccessor = httpContextAccessor;
            _appointmentSettings = appointmentSettings;
            _timeSlotSettings = timeSlotSettings;
        }
        public async Task<bool> Add(AppointmentsDTO model)
        {
            var doctorId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (doctorId == null) return false;
            if (model != null)
            {
                TimeSlot times;
                Dictionary<Day, List<TimeSlotDTO>> newTimes = model.times;

                foreach (var day in newTimes)
                {
                    Appointments appointments = new Appointments
                    {
                        doctorId = doctorId,
                        Price = model.price,
                        Day = day.Key
                    };
                    await _appointmentSettings.Add(appointments);
                    foreach (var time in day.Value)
                    {
                        times = new TimeSlot
                        {
                            time = time.time,
                            appointmentId = appointments.ID

                        };
                        await _timeSlotSettings.Add(times);
                    };
                }
                return true;

            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var existTimeSlot = await _timeSlotSettings.Delete(id);
            return existTimeSlot;
        }

        public async Task<bool> Update(TimeDTO entity)
        {
            TimeSlot time = new TimeSlot
            {
                timeId  = entity.id,
                time = entity.time
            };
            var existTimeSlot = await _timeSlotSettings.Update(time);
            return existTimeSlot;
        }


    }
}
