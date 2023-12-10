using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.DTOs
{
    public class RequestDTO
    {
        public int timeId { get; set; }
        public string DiscountCode { get; set; }
        public decimal price { get; set; }
        public decimal finalPrice { get; set; }
        public Status status { get; set; } = Status.Pending;
        public int sepcializationId { get; set; }
        public string PatientId { get; set; }
        
    }
    public class PatientRequestDTO
    {
        public byte[] image { get; set; }
        public string doctorName { get; set; }
        public string specialize { get; set; }
        public string day { get; set; }
        public TimeSpan time { get; set; }
        public decimal price { get; set; }
        public string discountCode { get; set; }
        public decimal finalPrice { get; set; }
        public string status { get; set; }
    }
}
