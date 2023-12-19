
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
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
        private readonly JWT tokenOptions;
        public RegistrationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IRepository<Doctor, int> _doctorContext, IOptions<JWT> tokenOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            doctorContext = _doctorContext;
            this.tokenOptions = tokenOptions.Value;
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
                    {
                        var jwtSecurityToken=await GenerateJwtToken(user);
                        var generatedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                        return true;
                    }
                    else
                    {
                        await doctorContext.Delete(doctorModel.Id);
                    }
                }
            }
            else
            {
                await _userManager.DeleteAsync(user);
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new Exception(errors);
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
        public async Task<bool> UserRegisterAsync(UserRegistrationDTO model)
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
                {
                    var jwtSecurityToken = await GenerateJwtToken(user);
                    var generatedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    return true;
                }
            }
            else
            {
                var errors = string.Empty;
                foreach (var error in res.Errors)
                {
                    errors += error.Description;
                }
                throw new Exception(errors);
            }
            return false;
        }
        public async Task<SignInResult> LoginAsync(SignInDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user != null)
            {
                var token = await GenerateJwtToken(user);
                var resultedToken =   new JwtSecurityTokenHandler().WriteToken(token);
                return await _signInManager.PasswordSignInAsync(user, model.password, isPersistent: false, lockoutOnFailure: false);
            }
            return SignInResult.Failed;
        }
        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            var userRole = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in userRole)
            {
                roleClaims.Add(new Claim("Role", role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            //generate token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(tokenOptions.ExpiredDuration),
                signingCredentials: signingCredentials
            );
            return jwtSecurityToken;
        }

    }
}
