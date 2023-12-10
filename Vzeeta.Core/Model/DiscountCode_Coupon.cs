using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Core.Model
{
    public class DiscountCode_Coupon
    {
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int NoOfRequests { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        [Required]
        public decimal Value { get; set; }
        public bool IsExpired { get; set; } = false;
    }
}
