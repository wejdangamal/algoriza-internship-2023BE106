using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.DTOs
{
    //[{fullName,#requests}]
    public class TopSpecializationDTO
    {
        public string SpecializationName { get; set; }
        public int requests { get; set; }
    }
}
