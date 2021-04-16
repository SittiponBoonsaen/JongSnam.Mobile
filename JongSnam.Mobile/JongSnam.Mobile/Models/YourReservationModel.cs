using System;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.Models
{
    public class YourReservationModel : ReservationDto
    {
        public string ApprovalStatusString { get; set; }
        public TimeSpan StartTimePicker { get; set; }
        public TimeSpan StopTimePicker { get; set; }

        public System.DateTime DateTime { get; set; }
        public ImageSource ImageSource { get; set; }
        public bool IsApproved { get; set; }
        public bool UnApproved { get; set; }
        public bool IsVisible { get; set; }
    }
}
