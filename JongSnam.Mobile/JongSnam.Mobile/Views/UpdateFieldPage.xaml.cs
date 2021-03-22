using JongSnam.Mobile.ViewModels;
using JongSnamService.Models;
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
    public partial class UpdateFieldPage : ContentPage
    {
        UpdateFieldViewModel _viewModel;
        public UpdateFieldPage(FieldDto fieldDto)
        {
            InitializeComponent();
            BindingContext = _viewModel = new UpdateFieldViewModel(fieldDto);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}