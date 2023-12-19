using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;

namespace Vzeeta.Core.ViewModels
{
    public class UserRegistrationDTO: BaseRegistrationDTO
    {
        public IFormFile? Image { get; set; }
    }
}
