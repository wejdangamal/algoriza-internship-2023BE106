using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Core.Repository;
using Vzeeta.Core.Service;
using Vzeeta.Services.Interfaces.IAdmin;

namespace Vzeeta.Services.Services.AdminServices
{
    public class AdminDoctorBoard : IAdminDoctorBoard
    {
        private readonly IRepository<Doctor, int> repository;
        private readonly ICustomService<Doctor> customService;
        private readonly IRepository<ApplicationUser, string> applicationUserContext;
        private readonly IRepository<Booking, int> bookingContext;

        public AdminDoctorBoard(IRepository<Doctor, int> repository, ICustomService<Doctor> customService, IRepository<ApplicationUser, string> applicationUserContext, IRepository<Booking, int> bookingContext)
        {
            this.repository = repository;
            this.customService = customService;
            this.applicationUserContext = applicationUserContext;
            this.bookingContext = bookingContext;
        }
        public async Task<bool> Delete(int id)
        {
            var entityFound = await customService.FindById(x => x.Id == id && x.ApplicationUsers.Role == UserRole.Doctor);
            if (entityFound != null)
            {
                List<TimeSlot> doctorAppointments = entityFound.Appointments.SelectMany(x => x.times).ToList();//ALL TimeSlots of Doctors
                List<int> doctorTimeSlotsId = doctorAppointments.Select(x => x.timeId).ToList();
                List<int> bookingResult = bookingContext.GetAllEntities().Where(x => x.status == Status.Pending).Select(x => x.timeId).ToList();
                if (doctorTimeSlotsId.Count > 0 && bookingResult.Count > 0)
                {
                    var res = doctorTimeSlotsId.Intersect(bookingResult);
                    if (res.Any())
                    {
                        throw new Exception("Can't Delete This Doctor, as Has Pending Booking");
                    }
                }
                else
                {
                    var IsDeleted = await applicationUserContext.Delete(entityFound.ApplicationUserId);
                    return IsDeleted;
                }

            }
            return false;
        }

        public IEnumerable<DocDTOS> GetAll(int page, int pageSize, Func<Doctor, bool> query, string[] includes = null)
        {
            var result = repository.GetAll(page, pageSize, query,includes)
                .Select(doctor=>new DocDTOS
                {
                    fullName = doctor.ApplicationUsers.UserName,
                    email = doctor.ApplicationUsers.Email,
                    dateOfBirth = doctor.ApplicationUsers.DateOfBirth,
                    gender = doctor.ApplicationUsers.gender.ToString(),
                    phone = doctor.ApplicationUsers.PhoneNumber,
                    specialize = doctor.specializations.specializeType,
                    image = doctor.ApplicationUsers.Image
                }).ToList();
            return result;
        }

        //[details:{image,fullName,email,phone,specialize,gender,dateOfBirth}]

        public async Task<DocDTOS> GetById(int Id)
        {
            var doctor = await customService.FindById(x => x.Id == Id && x.ApplicationUsers.Role == UserRole.Doctor);
            DocDTOS _resultDTO = null;
            if (doctor != null)
            {
                _resultDTO = new DocDTOS
                {
                    fullName = doctor.ApplicationUsers.UserName,
                    email = doctor.ApplicationUsers.Email,
                    dateOfBirth = doctor.ApplicationUsers.DateOfBirth,
                    gender = doctor.ApplicationUsers.gender.ToString(),
                    phone = doctor.ApplicationUsers.PhoneNumber,
                    specialize = doctor.specializations.specializeType,
                    image = doctor.ApplicationUsers.Image
                };
                return _resultDTO;
            }
            return _resultDTO;
        }
        public async Task<bool> Update(DoctorUpdateDTO entity)
        {
            var findDoctor = await customService.FindById(x => x.Id == entity.id && x.ApplicationUsers.Role == UserRole.Doctor);
            if (findDoctor != null)
            {
                findDoctor.ApplicationUsers.firstName = entity.firstName;
                findDoctor.ApplicationUsers.lastName = entity.lastName;
                findDoctor.ApplicationUsers.Email = entity.email;
                findDoctor.ApplicationUsers.DateOfBirth = entity.DateOfBirth;
                findDoctor.ApplicationUsers.PhoneNumber = entity.phone;
                findDoctor.ApplicationUsers.UserName = entity.firstName + " " + entity.lastName;
                findDoctor.ApplicationUsers.gender = entity.gender;

                if (entity.Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await entity.Image.CopyToAsync(memoryStream);

                        findDoctor.ApplicationUsers.Image = memoryStream.ToArray();

                    }
                }
                findDoctor.specializeId = entity.specializeId;
                var _result = await repository.Update(findDoctor);

                return _result;
            }
            else
            {
                throw new Exception("Not Found");
            }
        }

    }
}
