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

    public partial class StoreRequest
    {
        /// <summary>
        /// Initializes a new instance of the StoreRequest class.
        /// </summary>
        public StoreRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the StoreRequest class.
        /// </summary>
        public StoreRequest(int ownerId, string name, string address, int subDistrictId, int districtId, int provinceId, string contactMobile, bool isOpen, string image = default(string), double? latitude = default(double?), double? longtitude = default(double?), string officeHours = default(string), string rules = default(string))
        {
            OwnerId = ownerId;
            Image = image;
            Name = name;
            Address = address;
            SubDistrictId = subDistrictId;
            DistrictId = districtId;
            ProvinceId = provinceId;
            ContactMobile = contactMobile;
            Latitude = latitude;
            Longtitude = longtitude;
            OfficeHours = officeHours;
            IsOpen = isOpen;
            Rules = rules;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ownerId")]
        public int OwnerId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "subDistrictId")]
        public int SubDistrictId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "districtId")]
        public int DistrictId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "provinceId")]
        public int ProvinceId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contactMobile")]
        public string ContactMobile { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "longtitude")]
        public double? Longtitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "officeHours")]
        public string OfficeHours { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isOpen")]
        public bool IsOpen { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "rules")]
        public string Rules { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
            if (Address == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Address");
            }
            if (ContactMobile == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ContactMobile");
            }
            if (Name != null)
            {
                if (Name.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Name", 50);
                }
            }
            if (Address != null)
            {
                if (Address.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Address", 50);
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
            if (OfficeHours != null)
            {
                if (OfficeHours.Length > 150)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "OfficeHours", 150);
                }
            }
            if (Rules != null)
            {
                if (Rules.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Rules", 50);
                }
            }
        }
    }
}
