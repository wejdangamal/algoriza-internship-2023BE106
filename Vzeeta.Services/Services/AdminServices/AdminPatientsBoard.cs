using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Core.Service;
using Vzeeta.Services.Interfaces.IAdmin;

namespace Vzeeta.Services.Services.AdminServices
{
    public class AdminPatientsBoard : IAdminPatientsBoard
    {
        private IRepository<ApplicationUser, string> _repository;
        private readonly ICustomService<ApplicationUser> customService;
        private readonly IRepository<Booking, int> patientRequests;

        public AdminPatientsBoard(IRepository<ApplicationUser, string> repository,
            ICustomService<ApplicationUser> customService,
            IRepository<Booking, int> patientRequests)
        {
            _repository = repository;
            this.customService = customService;
            this.patientRequests = patientRequests;
        }
        public IEnumerable<PatientsDTO> GetAll(int page, int pageSize, Func<ApplicationUser, bool> query)
        {
            var _result = _repository.GetAll(page, pageSize, query)
                .Select(p => new PatientsDTO
                {
                    image = p.Image,
                    dateOfBirth = p.DateOfBirth,
                    email = p.Email,
                    fullName = p.UserName,
                    phone = p.PhoneNumber,
                    gender = p.gender.ToString()
                }).ToList();

            return _result;
        }
        public async Task<PatientDTO> GetById(string id)
        {
            var _result = await customService.FindById(x => x.Id == id && x.Role == UserRole.Patient);
            if (_result == null)
            {
                throw new Exception("Invaild Id");
            }
            var requestsOfPatient = patientRequests.GetAllEntities().Where(p => p.PatientId == id)
                .Select(x => new PatientRequestDTO
                {
                    finalPrice = x.finalPrice,
                    price = x.price,
                    status = x.status.ToString(),
                    discountCode = x.DiscountCode,
                    time = x.TimeSlot.time,
                    day = x.TimeSlot.appointments.Day.ToString(),
                    doctorName = x.TimeSlot.appointments.Doctor.ApplicationUsers.UserName,
                    specialize = x.TimeSlot.appointments.Doctor.specializations.specializeType,
                    image = x.TimeSlot.appointments.Doctor.ApplicationUsers.Image
                })
                .ToList();

            PatientDTO customPatient = new PatientDTO
            {
                details = new PatientsDTO
                {
                    dateOfBirth = _result.DateOfBirth,
                    email = _result.Email,
                    fullName = _result.UserName,
                    gender = _result.gender.ToString(),
                    phone = _result.PhoneNumber,
                    image = _result.Image
                },
                requests = requestsOfPatient
            };
            return customPatient;
        }
    }
}
