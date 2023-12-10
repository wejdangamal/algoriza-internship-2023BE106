using Microsoft.AspNetCore.Identity;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IAdmin;

namespace Vzeeta.Services.Services.AdminServices
{
    public class AdminDashBoard : IAdminDashBoard
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Booking, int> requestRepository;

        public AdminDashBoard(UserManager<ApplicationUser> userManager, IRepository<Booking, int> requestRepository)
        {
            _userManager = userManager;
            this.requestRepository = requestRepository;

        }
        public int NumOfDoctors()
        {
            var numOfDoctor = _userManager.GetUsersInRoleAsync(UserRole.Doctor.ToString()).Result.Count;

            return numOfDoctor;
        }

        public int NumOfPatients()
        {
            var numOfPatients = _userManager.GetUsersInRoleAsync(UserRole.Patient.ToString()).Result.Count;
            return numOfPatients;
        }

        public IEnumerable<BookingDTO> NumOfRequests()
        {
            var Total = requestRepository.GetAllEntities().Count();
            var result = requestRepository.GetAllEntities()
                .GroupBy(x => x.status)
                .Select(m => new BookingDTO { TotalRequests = Total, Request = m.Key.ToString(), NumberOfRequests = m.Count() });
            return result;
        }

        public IEnumerable<TopSpecializationDTO> TopFiveSpecializations()
        {
            var topSpecializations = requestRepository.GetAllEntities()
                .GroupBy(x => x.specialization)
                .Select(s => new TopSpecializationDTO
                {
                    SpecializationName = s.Key,
                    requests = s.Count()
                })
                .OrderByDescending(s=>s.requests)
                .Take(5);
            return topSpecializations;
        }
        public IEnumerable<TopDoctorsDTO> TopTenDoctors()
        {
            var testBoooking = requestRepository.GetAllEntities()
                .GroupBy(x => x.TimeSlot.appointments.doctorId)
                .Select(s => new TopDoctorsDTO
                {
                    specialize = s.Select(x=>x.TimeSlot.appointments.Doctor.specializations.specializeType).FirstOrDefault(),
                    requests = s.Count(),
                    fullName = s.Select(s=>s.TimeSlot.appointments.Doctor.ApplicationUsers.UserName).FirstOrDefault(),
                    image = s.Select(x => x.TimeSlot.appointments.Doctor.ApplicationUsers.Image).FirstOrDefault()
                }).OrderByDescending(s => s.requests)
                .ThenBy(s=>s.fullName)
                .Take(10);
            return testBoooking;
        }
    }
}
