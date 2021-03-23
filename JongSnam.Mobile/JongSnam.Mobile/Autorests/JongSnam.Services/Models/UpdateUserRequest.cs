// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace JongSnamServices.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class UpdateUserRequest
    {
        /// <summary>
        /// Initializes a new instance of the UpdateUserRequest class.
        /// </summary>
        public UpdateUserRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UpdateUserRequest class.
        /// </summary>
        public UpdateUserRequest(string email, string firstName, string lastName, string contactMobile, string address = default(string), string imageProfile = default(string))
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            ContactMobile = contactMobile;
            ImageProfile = imageProfile;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contactMobile")]
        public string ContactMobile { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "imageProfile")]
        public string ImageProfile { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Email == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Email");
            }
            if (FirstName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "FirstName");
            }
            if (LastName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "LastName");
            }
            if (ContactMobile == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ContactMobile");
            }
            if (Email != null)
            {
                if (Email.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Email", 50);
                }
            }
            if (FirstName != null)
            {
                if (FirstName.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "FirstName", 50);
                }
            }
            if (LastName != null)
            {
                if (LastName.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "LastName", 50);
                }
            }
            if (Address != null)
            {
                if (Address.Length > 150)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Address", 150);
                }
            }
            if (ContactMobile != null)
            {
                if (ContactMobile.Length > 10)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "ContactMobile", 10);
                }
                if (ContactMobile.Length < 9)
                {
                    throw new ValidationException(ValidationRules.MinLength, "ContactMobile", 9);
                }
            }
        }
    }
}
