using System;
using System.Collections.Generic;
using System.Text;
using JongSnamService.Models;

namespace JongSnam.Mobile.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public CommentViewModel(ReviewDto reviewDto)
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
