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

        private int _userId = Convert.ToInt32(Preferences.Get(AuthorizeConstants.UserIdKey, null));
        private string _address;

        public ValidatableObject<string> FirstName { get; set; }

        public ValidatableObject<string> LastName { get; set; }

        public ValidatableObject<string> Email { get; set; }

        public ValidatableObject<string> Phone { get; set; }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public ValidatableObject<ImageSource> ImageProfile { get; set; }

        public Command LoadItemsCommand { get; private set; }

        public Command ChangePasswordCommand { get; private set; }

        public Command SaveCommand { get; private set; }

        public Command UploadImageCommand { get; private set; }

        public Command LogoutCommand { get; private set; }

        public Command FirstNameTextChangedCommand { get; private set; }

        public Command LastNameTextChangedCommand { get; private set; }

        public Command EmailTextChangedCommand { get; private set; }

        public Command PhoneTextChangedCommand { get; private set; }

        public YourProFileViewModel()
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            _authenticationServices = DependencyService.Get<IAuthenticationServices>();

            InitValidation();

            InitOnChange();

            SetupCommands();
        }

        void SetupCommands()
        {
            Task.Run(async () => await ExecuteLoadItemsCommand(_userId));

            SaveCommand = new Command(async () => await ExecuteSaveCommand(_userId));

            ChangePasswordCommand = new Command(OnChangePassword);

            UploadImageCommand = new Command(async () => await SetupActionSheetForCamera());

            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());
        }

        void InitValidation()
        {
            ImageProfile = new ValidatableObject<ImageSource>();
            ImageProfile.Validations.Add(new IsHaveImageRule { OriginalFile = ImageConstants.NoImageAvailable, ValidationMessage = MessageConstants.PleaseAddImage });

            FirstName = new ValidatableObject<string>();
            FirstName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = MessageConstants.PleaseFillLastName });
            
            LastName = new ValidatableObject<string>();
            LastName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = MessageConstants.PleaseFillLastName });
            
            Email = new ValidatableObject<string>();
            Email.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = MessageConstants.PleaseFillEmail });
            
            Phone = new ValidatableObject<string>();
            Phone.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = MessageConstants.PleaseFillPhone });
        }

        void InitOnChange()
        {
            FirstNameTextChangedCommand = new Command(() => FirstName.Validate());

            LastNameTextChangedCommand = new Command(() => LastName.Validate());

            EmailTextChangedCommand = new Command(() => Email.Validate());

            PhoneTextChangedCommand = new Command(() => Phone.Validate());
        }

        async Task ExecuteLoadItemsCommand(int id)
        {
            IsBusy = true;
            try
            {

                await _usersServices.GetUserById(
                    id,
                    executeSuccess: (user) =>
                    {
                        FirstName.Value = user.FirstName;
                        LastName.Value = user.LastName;
                        Email.Value = user.Email;
                        Phone.Value = user.ContactMobile;
                        Address = user.Address;

                        if (!string.IsNullOrEmpty(user.Image))
                        {
                            ImageProfile.Value = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(user.Image)));
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
                if (!IsValid())
                {
                    return;
                }

                bool answer = await Shell.Current.DisplayAlert(MessageConstants.Noti, MessageConstants.WantToEdit, "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }

                var imageStream = await ((StreamImageSource)ImageProfile.Value).Stream.Invoke(new System.Threading.CancellationToken());

                var request = new UpdateUserRequest
                {
                    LastName = LastName.Value,
                    FirstName = FirstName.Value,
                    Email = Email.Value,
                    ContactMobile = Phone.Value,
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

        bool IsValid()
        {
            return LastName.Validate();
        }

        async Task SetupActionSheetForCamera()
        {
            var actionSheet = await Shell.Current.DisplayActionSheet(MessageConstants.UploadImage, MessageConstants.Cancel, null, MessageConstants.Camera, MessageConstants.Gallery);

            switch (actionSheet)
            {
                case MessageConstants.Camera:

                    await TakePhotoAsync();

                    break;

                case MessageConstants.Gallery:

                    await PickPhotoAsync();

                    break;

            }
        }

        async void OnChangePassword()
        {
            await Shell.Current.Navigation.PushAsync(new ChangePasswordPage(_userId));
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
            bool answer = await Shell.Current.DisplayAlert(MessageConstants.Noti, MessageConstants.WanToLogout, "ใช่", "ไม่");
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
                await Shell.Current.DisplayAlert(MessageConstants.Noti, MessageConstants.CannotLogout, MessageConstants.Ok);
            }
        }
    }
}
