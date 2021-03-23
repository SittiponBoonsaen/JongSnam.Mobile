using System;
using System.Collections.Generic;
using System.Text;
using JongSnamService.Models;

namespace JongSnam.Mobile.Models
{
    public class YourReservationModel : ReservationDto
    {
        public string ApprovalStatusString { get; set; }
        public string StartTimeString { get; set; }
        public string StopTimeString { get; set; }
    }
}
