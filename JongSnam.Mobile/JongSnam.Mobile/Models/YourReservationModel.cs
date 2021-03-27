using System;
using JongSnamService.Models;

namespace JongSnam.Mobile.Models
{
    public class YourReservationModel : ReservationDto
    {
        public string ApprovalStatusString { get; set; }
        public TimeSpan StartTimePicker { get; set; }
        public TimeSpan StopTimePicker { get; set; }

        public System.DateTime DateTime { get; set; }
    }
}
