using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.Model
{
    public class Specialization
    {
        public int ID { get; set; }
        public string specializeType { get; set; }
        public string Description { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
    }
}
