using System;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private readonly IUsersServices _usersServices;

        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        private string _oldPassword;
        private string _newPassword;
        private string _confirmNewPassword;

        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }
        public string ConfirmNewPassword
        {
            get => _confirmNewPassword;
            set
            {
                _confirmNewPassword = value;
                OnPropertyChanged(nameof(ConfirmNewPassword));
            }
        }

        public ChangePasswordViewModel(int idUser)
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            CancelCommand = new Command(OnCancel);

            SaveCommand = new Command(async () => await OnSave(idUser));
        }

        async Task OnSave(int idUser)
        {
            try
            {
                bool answer = await Shell.Current.DisplayAlert("Question?", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "Yes", "No");
                if (!answer)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmNewPassword))
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                    return;
                }
                else if (NewPassword != ConfirmNewPassword)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "รหัสผ่านไม่ตรงกัน", "ตกลง");
                    return;
                }
                var request = new ChangePasswordRequest
                {
                    OldPassword = OldPassword,
                    NewPassword = NewPassword,
                    ConfirmNewPassword = ConfirmNewPassword
                };
                var statusSaved = await _usersServices.ChangePassword(idUser, request);
                if (statusSaved)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
                }
                
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                return;
                throw ex;
            }
            finally
            {
                IsBusy = true;
            }

        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
