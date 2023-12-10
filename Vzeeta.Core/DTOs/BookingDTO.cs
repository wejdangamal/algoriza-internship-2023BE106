using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model;

namespace Vzeeta.Core.DTOs
{
    public class BookingDTO
    {
        public int TotalRequests { get; set; }
        public string Request { get; set; }
        public int NumberOfRequests { get; set; }
    }
}
