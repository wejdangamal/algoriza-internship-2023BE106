using System.ComponentModel.DataAnnotations;

namespace Vzeeta.Core.DTOs
{
    public class TimeDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public TimeSpan time { get; set; }
    }

}
