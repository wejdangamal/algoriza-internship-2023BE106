using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.Model
{
    public class Appointments
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public Day Day { get; set; }
        public virtual List<TimeSlot> times { get; set; }
        public string doctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
    public class TimeSlot
    {
        [Key]
        public int timeId { get; set; }
        public TimeSpan time { get; set; }
        [ForeignKey("appointments")]
        public int appointmentId { get; set; }
        public virtual Appointments appointments { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}
