using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public Command DeleteFieldCommand { get; }
        public Command SaveCommand { get; }

        private FieldDetailDto _fieldDto { get; set; }

        private ValidatableObject<EnumDto> _selectedIsOpen;

        public ValidatableObject<EnumDto> SelectedIsOpen
        {
            get
            {
                return _selectedIsOpen;
            }

            set
            {
                _selectedIsOpen = value;
                OnPropertyChanged(nameof(SelectedIsOpen));
            }
        }
        public Command SelectedIsOpenIndexChangedCommand { get; private set; }
        public List<EnumDto> IsOpenValues { get; set; }

        private string _name;
        private string _size;
        private double _price;
        private string _isOpenString;
        private double _percentage;
        private List<EnumDto> _privacy;
        private string _sizeField;
        private DateTime _dateNow;
        private DateTime _startDate;
        private DateTime _endDate;


        public IEnumerable<UpdateDiscountRequest> _updateDiscountRequests;
        public IEnumerable<UpdatePictureFieldRequest> _updatePictureFieldRequest;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private bool _isOpenbool;
        private string _detail;
        private ImageSource _imageProfile;

        public string IsOpenString
        {
            get => _isOpenString;
            set
            {
                _isOpenString = value;
                OnPropertyChanged(nameof(IsOpenString));
            }
        }
        public double Percentage
        {
            get => _percentage;
            set
            {
                _percentage = value;
                OnPropertyChanged(nameof(Percentage));
            }
        }
        public System.DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public System.DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public string Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                OnPropertyChanged(nameof(Detail));
            }
        }



        public IEnumerable<UpdateDiscountRequest> UpdateDiscountRequest
        {
            get => _updateDiscountRequests;
            set
            {
                _updateDiscountRequests = value;
                OnPropertyChanged(nameof(UpdateDiscountRequest));
            }
        }
        public IEnumerable<UpdatePictureFieldRequest> UpdatePictureFieldRequest
        {
            get => _updatePictureFieldRequest;
            set
            {
                _updatePictureFieldRequest = value;
                OnPropertyChanged(nameof(UpdatePictureFieldRequest));
            }
        }



        public List<EnumDto> Privacy
        {
            get
            {
                return _privacy;
            }
            set
            {
                _privacy = value;
                OnPropertyChanged(nameof(Privacy));
            }
        }

        public string SizeField
        {
            get => _sizeField;
            set
            {
                _sizeField = value;
                OnPropertyChanged(nameof(SizeField));
            }
        }

        public System.DateTime DateNow
        {
            get => _dateNow;
            set
            {
                _dateNow = value;
                OnPropertyChanged(nameof(DateNow));
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

        public Command UploadImageCommand { get; private set; }

        public UpdateFieldViewModel(FieldDto fieldDto, int storeId)
        {
            IsOpenValues = new List<EnumDto>
            {
                new EnumDto
                {
                    Id = 1,
                    Name = "เปิดบริการ"
                },
                new EnumDto
                {
                    Id = 2,
                    Name = "ปิดบริการ"
                }
            };

            _selectedIsOpen = new ValidatableObject<EnumDto>();
            _fieldServices = DependencyService.Get<IFieldServices>();

            DeleteFieldCommand = new Command(async () => await OnDeleteFieldCommandAlertYesNoClicked(fieldDto.Id.Value));

            SaveCommand = new Command(async () => await OnSaveCommandAlertYesNoClicked(fieldDto.Id.Value, storeId));

            Task.Run(async () => await ExecuteLoadItemsCommand(fieldDto.Id.Value));

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
        async Task ExecuteLoadItemsCommand(int fieldId)
        {
            IsBusy = true;
            try
            {
                DateNow = DateTime.Now;
                _fieldDto = await _fieldServices.GetFieldById(fieldId);
                Name = _fieldDto.Name;
                Price = _fieldDto.Price.Value;
                SizeField = _fieldDto.Size;
                SelectedIsOpen.Value = _fieldDto.IsOpen.Value ? IsOpenValues.Find(f => f.Id.Value == 1) : IsOpenValues.Find(f => f.Id.Value == 2);
                Percentage = _fieldDto.DiscountModel.Percentage == null ? 0 : _fieldDto.DiscountModel.Percentage.Value;
                StartDate = _fieldDto.DiscountModel.StartDate == null ? DateNow : _fieldDto.DiscountModel.StartDate.Value;
                EndDate = _fieldDto.DiscountModel.EndDate.Value == null ? _fieldDto.DiscountModel.EndDate.Value : _fieldDto.DiscountModel.EndDate.Value;
                Detail = _fieldDto.DiscountModel.Detail == null ? " ": _fieldDto.DiscountModel.Detail;
                if (_fieldDto.ImageFieldModel.Count > 0)
                {
                    ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_fieldDto.ImageFieldModel[0].Image)));
                }
                else
                {
                    ImageProfile = ImageSource.FromUri(new Uri("https://image.makewebeasy.net/makeweb/0/xOIgxrdh9/Document/Compac_spray_small_size_1.pdf"));
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

        public void OnAppearing()
        {
            IsBusy = true;
        }
        async Task OnDeleteFieldCommandAlertYesNoClicked(int fieldId)
        {
            bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการที่ลบสนามนี้ใช่หรือไม่ ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }
            IsBusy = true;
            var statusSaved = await _fieldServices.DeleteField(fieldId);
            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกลบเรียบร้อยแล้ว", "ตกลง");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถลบข้อมูลได้", "ตกลง");
            }
        }

        async Task OnSaveCommandAlertYesNoClicked(int fieldId, int storeId)
        {
            try 
            {
                IsBusy = true;
                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน?", "ต้องการที่จะแก้ไขข้อมูลใช่ไหม ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }
                IsBusy = true;

                var imageStream = await ((StreamImageSource)ImageProfile).Stream.Invoke(new System.Threading.CancellationToken());

                var request = new UpdateFieldRequest
                {
                    Active = _fieldDto.Active,
                    Name = Name,
                    IsOpen = SelectedIsOpen.Value.Id.Value == 1 ? true : false,
                    Price = (int)Price,
                    Size = SizeField,
                    UpdateDiscountRequest = new UpdateDiscountRequest
                    {
                        Detail = Detail,
                        Percentage = Percentage,
                        StartDate = StartDate,
                        EndDate = EndDate,
                        Id = _fieldDto.DiscountModel.Id
                    },

                    UpdatePictureFieldRequest = new List<UpdatePictureFieldRequest>
                {
                    new UpdatePictureFieldRequest
                    {
                        Id = _fieldDto.ImageFieldModel[0].Id,
                        Image = await GeneralHelper.GetBase64StringAsync(imageStream)
                    }
                },
                    StoreId = storeId
                    //ไม่มีสถานะร้าน
                };
                var statusSaved = await _fieldServices.UpdateField(fieldId, request);

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
            }
            finally
            {
                IsBusy = false;
            }
            
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
