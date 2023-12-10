using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.Model
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("specializations")]
        public int specializeId { get; set; } 
        public virtual Specialization specializations { get; set; } 
        public virtual List<Appointments> Appointments { get; set; }
        
        [ForeignKey("ApplicationUsers")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }
    }
}
