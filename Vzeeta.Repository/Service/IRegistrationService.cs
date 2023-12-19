using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;
using Vzeeta.Core.ViewModels;

namespace Vzeeta.Core.Service
{
    public interface IRegistrationService
    {
        Task<bool> DoctorRegisterAsync(DoctorRegistrationDTO model);
        Task<SignInResult> LoginAsync(SignInDTO model);
        Task<bool> UserRegisterAsync(UserRegistrationDTO model);
    }
}
