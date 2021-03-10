using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class StoreRequest
    {

        public int OwnerId { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string Amphur { get; set; }

        public string Province { get; set; }

        public string ContactMobile { get; set; }
 
        public decimal? Latitude { get; set; }

        public decimal? Longtitude { get; set; }

        public string OfficeHours { get; set; }

        public bool IsOpen { get; set; }

        public string Rules { get; set; }



    }
}
