using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.Model
{
    public class ApplicationUser : IdentityUser
    {
        public virtual byte[]? Image { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Gender gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }

}


