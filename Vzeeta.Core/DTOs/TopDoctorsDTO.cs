using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.DTOs
{
    //[{image,fullName,specialize,#requests}]
    public class TopDoctorsDTO
    {
        public byte[] image { get; set; }
        public string fullName { get; set; }
        public string specialize { get; set; }
        public int requests { get; set; }

    }
}
