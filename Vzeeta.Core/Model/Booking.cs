using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.Model
{
    public class Booking
    {
        public int ID { get; set; }
        [ForeignKey("TimeSlot")]
        public int timeId { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }
        public string? DiscountCode { get; set; }
        public decimal price { get; set; }
        public decimal finalPrice { get; set; }
        public Status status { get; set; } = Status.Pending;       
        public string specialization { get; set; }   
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public virtual ApplicationUser  Patient { get; set; }
    }
}
