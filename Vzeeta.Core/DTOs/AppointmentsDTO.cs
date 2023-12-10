using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.DTOs
{
    public class AppointmentsDTO
    {
        public decimal price { get; set; }
        public Dictionary<Day, List<TimeSlotDTO>> times { get; set; }
    }
    public class TimeSlotDTO
    {
        public TimeSpan time { get; set; }
    }

}
