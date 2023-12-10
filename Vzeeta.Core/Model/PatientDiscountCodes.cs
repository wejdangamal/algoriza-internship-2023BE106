using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.Model
{
    public class PatientDiscountCodes
    {
        [Column(Order = 0), ForeignKey("Code")]
        public int dicountCodeID { get; set; }
        public virtual DiscountCode_Coupon Code { get; set; }
        [Column(Order = 1), ForeignKey("patientID")]    
        public string patientID { get; set; }
        public virtual ApplicationUser patient { get; set; }
        public bool UsedCode { get; set; }
    }
}
