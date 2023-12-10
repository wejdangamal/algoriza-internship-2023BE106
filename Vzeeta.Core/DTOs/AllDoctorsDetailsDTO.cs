namespace Vzeeta.Core.DTOs
{
    //[ {image,fullName,email,phone,specialize,price,gender,appointments:[{day, times:[{id, time}]}]}]
    public class AllDoctorsDetailsDTO
    {
        public DocDTOS details { get; set; }
        public decimal price { get; set; }
        public List<AppointmentsNDTO> appointments { get; set; }
    }
    public class AppointmentsNDTO
    {
        public string day { get; set; }
        public List<TimeSpanDTO> times { get; set; }
    }
    public class TimeSpanDTO
    {

        public int id { get; set; }
        public TimeSpan time { get; set; }
    }
}
