
using Microsoft.AspNetCore.Identity;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Core.Service;
using Vzeeta.Core.ViewModels;

namespace Vzeeta.Services.Services.AccountService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<Doctor, int> doctorContext;
        public RegistrationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRepository<Doctor, int> _doctorContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            doctorContext = _doctorContext;
        }
        public async Task<bool> DoctorRegisterAsync(DoctorRegistrationDTO doctor)
        {
            var user = createDoctorAsApplicationUser(doctor);
            if (doctor.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await doctor.Image.CopyToAsync(memoryStream);

                    user.Image = memoryStream.ToArray();

                }
            }
            var result = await _userManager.CreateAsync(user, doctor.password);
            if (result.Succeeded)
            {
                var doctorModel = new Doctor
                {
                    ApplicationUserId = user.Id,
                    specializeId = doctor.specializeId
                };
                var doctorAdded = await doctorContext.Add(doctorModel);
                if (doctorAdded)
                {
                    var addToDoctorRole = await addToRole(user, UserRole.Doctor.ToString());
                    if (addToDoctorRole)
                        return true;
                    else
                    {
                        await doctorContext.Delete(doctorModel.Id);
                    }
                }
            }
            else
            {
                await _userManager.DeleteAsync(user);
            }
            return false;
        }
        private ApplicationUser createDoctorAsApplicationUser(DoctorRegistrationDTO doctor)
        {
            var user = new ApplicationUser
            {
                UserName = $"{doctor.firstName}_{doctor.lastName}",
                Email = doctor.email,
                PasswordHash = doctor.password,
                DateOfBirth = doctor.DateOfBirth,
                PhoneNumber = doctor.phone,
                gender = doctor.gender,
                firstName = doctor.firstName,
                lastName = doctor.lastName,
                Role = UserRole.Doctor
            };

            return user;
        }
        private async Task<bool> addToRole(ApplicationUser user, string Role)
        {
            var added = await _userManager.AddToRoleAsync(user, Role);
            if (added.Succeeded)
                return true;
            else
            {
                await _userManager.DeleteAsync(user);
            }
            return false;
        }
        public async Task<bool> UserRegisterAsync(UserRegistrationVM model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = $"{model.firstName}_{model.lastName}",
                Email = model.email,
                PasswordHash = model.password,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.phone,
                gender = model.gender,
                firstName = model.firstName,
                lastName = model.lastName,
                Role = UserRole.Patient
            };
            if (model.Image != null && model.Image?.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(memoryStream);

                    user.Image = memoryStream.ToArray();

                }
            }
            var res = await _userManager.CreateAsync(user, model.password);
            if (res.Succeeded)
            {
                var addToPatientRole = await addToRole(user, UserRole.Patient.ToString());
                if (addToPatientRole)
                    return true;
            }
            return false;
        }
        public async Task<SignInResult> LoginAsync(SignInVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user != null)
            {
                return await _signInManager.PasswordSignInAsync(user, model.password, isPersistent: false, lockoutOnFailure: false);
            }
            return SignInResult.Failed;
        }

    }
}
