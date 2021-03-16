﻿using JongSnam.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourProFilePage : ContentPage
    {
        YourProFileViewModel _viewModel;

        public YourProFilePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new YourProFileViewModel(4);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}