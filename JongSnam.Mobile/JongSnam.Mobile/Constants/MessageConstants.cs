using System;
using System.Collections.Generic;
using System.Text;

namespace JongSnam.Mobile.Constants
{
    public static class MessageConstants
    {
        #region Exceptions
        public static string SomeValueINvalid = "ข้อมูลบางส่วนไม่ถูกต้อง";
        public static string UnauthorizedError = "การยืนยันตัวตนล้อมเหลว";
        public static string SomethingWentWrong = "เกิดข้อผิดพลาดบางอย่างจากระบบ";
        public static string CannotConnectToInternet = "ไม่มีการเชื่อต่ออินเตอร์เน็ต";
        #endregion

        #region Validations
        public static string PleaseSelectProvince = "กรุณาเลือกจังหวัด";
        public static string PleaseSelectDistrict = "กรุณาเลือกอำเภอ";
        public static string PleaseSelectSubDistrict = "กรุณาเลือกตำบล";
        public static string PleaseFillStoreName = "กรุณาใส่ชื่อร้าน";
        public static string PleaseFillAddress = "กรุณาใส่ที่อยู่";
        public static string PleaseFillContactMobile = "กรุณาใส่เบอร์ติดต่อ";
        public static string PleaseFillOfficeHour = "กรุณาใส่เวลาเปิดปิดทำการ";
        public static string PleaseAddImage = "กรุณาใส่รูป";

        #endregion

        #region Camara and Gallery
        public static string CannotAccessCamera = "ไม่สามารถใช้กล้องได้";
        public static string CameraNeedPermission = "กล้องใช้ไม่ได้ต้องการสิทธิ์ในการเข้าถึง";
        public static string NotSupportThisCamera = "แอพนี้ไม่รองรับการใช้งานกล้องของเครื่องนี้";

        public static string CannotChooseImage = "ไม่สามารถเลือกรูป";

        #endregion

        public static string Ok = "ตกลง";
        public static string Noti = "แจ้งเตือน!";
        public static string Directory = "JongSnam";
        public static string WantToEdit = "ต้องการที่จะแก้ไขใช่หรือไม่ ?";
        public static string CannotSave = "ไม่สามารถบันทึกข้อมูลได้";
        public static string SaveSuccessfully = "ข้อมูลถูกบันทึกเรียบร้อยแล้ว";


    }
}
