using System;
using System.Collections.Generic;
using System.Text;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.Models
{
    public class ReviewDtoModel : ReviewDto
    {
        public ImageSource ImageStar1 { get; set; }
        public ImageSource ImageStar2 { get; set; }
        public ImageSource ImageStar3 { get; set; }
        public ImageSource ImageStar4 { get; set; }
        public ImageSource ImageStar5 { get; set; }
    }
}
