using System;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourProFileViewModel : BaseViewModel
    {

        private readonly IUsersServices _usersServices;

        private string _firstName;
        private string _lastName;
        private string _emailName;
        private string _phone;
        private string _address;

        private ImageSource _imageProfile;

        public byte[] ImageProfuke { get; set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get => _emailName;
            set
            {
                _emailName = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public ImageSource ImageProfile 
        {
            get { return _imageProfile; }
            set
            {
                _imageProfile = value;
                OnPropertyChanged(nameof(ImageProfile));
            }
        }

        public UserDto DataUser { get; set; }

        public Command LoadItemsCommand { get; }

        public Command ChangePasswordCommand { get; }
        public Command SaveCommand { get; }

        public YourProFileViewModel(int id)
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            DataUser = new UserDto();

            Task.Run(async () => await ExecuteLoadItemsCommand(id));

            SaveCommand = new Command(async () => await ExecuteSaveCommand(id));

            ChangePasswordCommand = new Command(OnChangePassword);
        }

        async Task ExecuteLoadItemsCommand(int id)
        {
            IsBusy = true;
            try
            {
                var dataUser = await _usersServices.GetUserById(id);
                FirstName = dataUser.FirstName;
                LastName = dataUser.LastName;
                Email = dataUser.Email;
                Phone = dataUser.ContactMobile;
                Address = dataUser.Address;
                DataUser = dataUser;
                var aaImageBase64 = "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";
                ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(aaImageBase64)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteSaveCommand(int id)
        {
            bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }

            var request = new UpdateUserRequest
            {
                LastName = LastName,
                FirstName = FirstName,
                Email = Email,
                ContactMobile = Phone,
                Address = Address
            };

            var statusSaved = await _usersServices.UpdateUser(id, request);

            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
            }
        }


        async void OnChangePassword()
        {
            await Shell.Current.Navigation.PushAsync(new ChangePasswordPage(DataUser.Id.Value));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
