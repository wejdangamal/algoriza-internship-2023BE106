using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vzeeta.Core.Model
{
    public class DiscountCode_Coupon
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int NoOfRequests { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public bool IsExpired { get; set; } = false;
    }
}
