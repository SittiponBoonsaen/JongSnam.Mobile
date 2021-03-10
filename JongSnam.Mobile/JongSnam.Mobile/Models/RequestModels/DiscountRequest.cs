using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class DiscountRequest
    {

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? Percentage { get; set; }

        public string Detail { get; set; }
    }
}
