using System;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

        public Command UploadImageCommand { get; private set; }

        public YourProFileViewModel(int id)
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            DataUser = new UserDto();

            Task.Run(async () => await ExecuteLoadItemsCommand(id));

            SaveCommand = new Command(async () => await ExecuteSaveCommand(id));

            ChangePasswordCommand = new Command(OnChangePassword);

            UploadImageCommand = new Command(async () =>
            {
                if (IsBusy)
                {
                    return;
                }

                var actionSheet = await Shell.Current.DisplayActionSheet("อัพโหลดรูปภาพ", "Cancel", null, "กล้อง", "แกลลอรี่");

                switch (actionSheet)
                {
                    case "Cancel":

                        // Do Something when 'Cancel' Button is pressed

                        break;

                    case "กล้อง":

                        await TakePhotoAsync();

                        break;

                    case "แกลลอรี่":

                        await PickPhotoAsync();

                        break;

                }
            });
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
                ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(dataUser.Image)));
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
            IsBusy = true;
            try
            {
                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }
                var imageStream = await ((StreamImageSource)ImageProfile).Stream.Invoke(new System.Threading.CancellationToken());

                var request = new UpdateUserRequest
                {
                    LastName = LastName,
                    FirstName = FirstName,
                    Email = Email,
                    ContactMobile = Phone,
                    Address = Address,
                    ImageProfile = await GeneralHelper.GetBase64StringAsync(imageStream),
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
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

        private async Task TakePhotoAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถใช้กล้องได้", "กล้องใช้ไม่ได้ต้องการสิทธิ์ในการเข้าถึง", "ตกลง");
                return;
            }

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถใช้กล้องได้", "แอพนี้ไม่รองรับการใช้งานกล้องของเครื่องนี้", "ตกลง");
                return;
            }

            //เอาไว้เช็คว่าออกมาจากกล้องหรือยัง
            //_isBackFromChooseImage = true;

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "JongSnam",
                DefaultCamera = CameraDevice.Rear,
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                // รูปได้ค่าตอนนี้เด้อ
                ImageProfile = ImageSource.FromStream(() => file.GetStream());
            }
            //เอาไว้เช็คว่าออกมาจากกล้องหรือยัง
            //_isBackFromChooseImage = false;
        }

        private async Task PickPhotoAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถเลือกรูป", "ไม่สามารถเลือกรูปได้", "ตกลง");
                return;
            }

            //เอาไว้เช็คว่าออกมาจากคลังภาพหรือยัง
            //_isBackFromChooseImage = true;

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                ImageProfile = ImageSource.FromStream(() => file.GetStream());
            }

            //เอาไว้เช็คว่าออกมาจากคลังภาพหรือยัง
            //_isBackFromChooseImage = false;
        }
    }
}
