using System;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.CustomErrors;
using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourProFileViewModel : BaseViewModel
    {

        private readonly IUsersServices _usersServices;
        private readonly IAuthenticationServices _authenticationServices;

        private string userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);

        private string _firstName;
        private string _lastName;
        private string _emailName;
        private string _phone;
        private string _address;


        private ValidatableObject<ImageSource> _imageProfile;


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

        public ValidatableObject<ImageSource> ImageProfile
        {
            get { return _imageProfile; }
            set
            {
                _imageProfile = value;
                OnPropertyChanged(nameof(ImageProfile));
            }
        }

        public UserDto DataUser { get; set; }

        public Command LoadItemsCommand { get; private set; }

        public Command ChangePasswordCommand { get; private set; }

        public Command SaveCommand { get; private set; }

        public Command UploadImageCommand { get; private set; }

        public Command LogoutCommand { get; private set; }

        public YourProFileViewModel()
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            _authenticationServices = DependencyService.Get<IAuthenticationServices>();

            InitValidation();

            SetupCommands();

            DataUser = new UserDto();
        }

        void SetupCommands()
        {
            Task.Run(async () => await ExecuteLoadItemsCommand(Convert.ToInt32(userId)));

            SaveCommand = new Command(async () => await ExecuteSaveCommand(Convert.ToInt32(userId)));

            ChangePasswordCommand = new Command(OnChangePassword);

            UploadImageCommand = new Command(async () =>
            {
                var actionSheet = await Shell.Current.DisplayActionSheet("อัพโหลดรูปภาพ", "Cancel", null, "กล้อง", "แกลลอรี่");

                switch (actionSheet)
                {
                    case "กล้อง":

                        await TakePhotoAsync();

                        break;

                    case "แกลลอรี่":

                        await PickPhotoAsync();

                        break;

                }
            });

            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());
        }

        void InitValidation()
        {
            _imageProfile = new ValidatableObject<ImageSource>();
            _imageProfile.Validations.Add(new IsNotNullOrEmptyRule<ImageSource>() { ValidationMessage = MessageConstants.PleaseAddImage });

        }

        async Task ExecuteLoadItemsCommand(int id)
        {
            IsBusy = true;
            try
            {

                await _usersServices.GetUserById(
                    id,
                    executeSuccess: (dataUser) =>
                    {
                        FirstName = dataUser.FirstName;
                        LastName = dataUser.LastName;
                        Email = dataUser.Email;
                        Phone = dataUser.ContactMobile;
                        Address = dataUser.Address;
                        DataUser = dataUser;

                        if (!(String.IsNullOrEmpty(dataUser.Image)))
                        {
                            ImageProfile.Value = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(dataUser.Image)));
                        }
                        else
                        {
                            ImageProfile.Value = ImageSource.FromUri(new Uri(ImageConstants.NoImageAvailable));
                        }
                    },
                    executeError: async (msg, ex) =>
                    {
                        IsBusy = false;
                        //await UserService.Logout();
                        await Shell.Current.Navigation.PopToRootAsync();
                        //await Shell.Current.GoToAsync("//LoginPage");
                });

                //var dataUser = await _usersServices.GetUserById(id);

            }
            catch (Exception)
            {

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
                bool answer = await Shell.Current.DisplayAlert(MessageConstants.Noti, MessageConstants.WantToEdit, "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }
 
                var imageStream = await ((StreamImageSource)ImageProfile.Value).Stream.Invoke(new System.Threading.CancellationToken());

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
                    await Shell.Current.DisplayAlert(MessageConstants.Noti, MessageConstants.SaveSuccessfully, MessageConstants.Ok);
                }
                else
                {
                    await Shell.Current.DisplayAlert(MessageConstants.Noti, MessageConstants.CannotSave, MessageConstants.Ok);
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

        public async Task OnAppearingAsync()
        {
            IsBusy = true;
            var isLoggedIn = Preferences.Get(AuthorizeConstants.IsLoggedInKey, string.Empty);
            if (isLoggedIn != "True")
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        async Task TakePhotoAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable)
            {
                await Shell.Current.DisplayAlert(MessageConstants.CannotAccessCamera, MessageConstants.CameraNeedPermission, MessageConstants.Ok);
                return;
            }

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await Shell.Current.DisplayAlert(MessageConstants.CannotAccessCamera, MessageConstants.NotSupportThisCamera, MessageConstants.Ok);
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = MessageConstants.Directory,
                DefaultCamera = CameraDevice.Rear,
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                ImageProfile.Value = ImageSource.FromStream(() => file.GetStream());
            }
            IsBusy = false;
        }

        async Task PickPhotoAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert(MessageConstants.CannotChooseImage, MessageConstants.CannotChooseImage, MessageConstants.Ok);
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                ImageProfile.Value = ImageSource.FromStream(() => file.GetStream());
            }
            IsBusy = false;
        }

        async Task ExecuteLogoutCommand()
        {
            bool answer = await Shell.Current.DisplayAlert(MessageConstants.Noti, "ต้องการออกจากระบบใช่หรือไม่ ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }
            var result = await _authenticationServices.Logut();

            if (result)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await Shell.Current.DisplayAlert(MessageConstants.Noti, "ไม่สามารถออกจากระบบได้ กรุณาลองใหม่ภายหลัง", MessageConstants.Ok);
            }
        }
    }
}
