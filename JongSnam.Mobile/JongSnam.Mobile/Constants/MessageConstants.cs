using System;
using System.Collections.Generic;
using System.Text;

namespace JongSnam.Mobile.Constants
{
    public static class MessageConstants
    {
        #region Exceptions
        public const string SomeValueINvalid = "ข้อมูลบางส่วนไม่ถูกต้อง";
        public const string UnauthorizedError = "การยืนยันตัวตนล้อมเหลว";
        public const string SomethingWentWrong = "เกิดข้อผิดพลาดบางอย่างจากระบบ";
        public const string CannotConnectToInternet = "ไม่มีการเชื่อต่ออินเตอร์เน็ต";
        #endregion

        #region Validations
        public const string PleaseSelectProvince = "กรุณาเลือกจังหวัด";
        public const string PleaseSelectDistrict = "กรุณาเลือกอำเภอ";
        public const string PleaseSelectSubDistrict = "กรุณาเลือกตำบล";
        public const string PleaseFillStoreName = "กรุณาใส่ชื่อร้าน";
        public const string PleaseFillAddress = "กรุณาใส่ที่อยู่";
        public const string PleaseFillContactMobile = "กรุณาใส่เบอร์ติดต่อ";
        public const string PleaseFillOfficeHour = "กรุณาใส่เวลาเปิดปิดทำการ";
        public const string PleaseAddImage = "กรุณาใส่รูป";
        public const string PleaseFillLastName = "กรุณาใส่นามสกุล";
        public const string PleaseFillFirstName = "กรุณาใส่ชื่อ";
        public const string PleaseFillEmail = "กรุณาใส่อีเมลล์";
        public const string PleaseFillPhone = "กรุณาใส่เบอร์ติดต่อ";

        #endregion

        #region Camara and Gallery
        public const string CannotAccessCamera = "ไม่สามารถใช้กล้องได้";
        public const string CameraNeedPermission = "กล้องใช้ไม่ได้ต้องการสิทธิ์ในการเข้าถึง";
        public const string NotSupportThisCamera = "แอพนี้ไม่รองรับการใช้งานกล้องของเครื่องนี้";

        public const string CannotChooseImage = "ไม่สามารถเลือกรูป";
        public const string UploadImage = "อัพโหลดรูปภาพ";
        public const string Camera = "กล้อง";
        public const string Gallery = "แกลลอรี่";

        #endregion

        public const string Ok = "ตกลง";
        public const string Cancel = "ยกเลิก";
        public const string Noti = "แจ้งเตือน!";
        public const string Directory = "JongSnam";
        public const string WantToEdit = "ต้องการที่จะแก้ไขใช่หรือไม่ ?";
        public const string CannotSave = "ไม่สามารถบันทึกข้อมูลได้";
        public const string SaveSuccessfully = "ข้อมูลถูกบันทึกเรียบร้อยแล้ว";
        public const string WanToLogout = "ต้องการออกจากระบบใช่หรือไม่ ?";
        public const string CannotLogout = "ไม่สามารถออกจากระบบได้ กรุณาลองใหม่ภายหลัง";


    }
}
