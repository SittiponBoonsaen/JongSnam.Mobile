using System.ComponentModel.DataAnnotations;

namespace JongSnamFootball.Entities.Request
{
    public class UserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string ContactMobile { get; set; }

        public string ImageProfile { get; set; }

        public int UserTypeId { get; set; }
    }
}
