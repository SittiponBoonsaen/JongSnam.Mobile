// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace JongSnamService.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class StoreDto
    {
        /// <summary>
        /// Initializes a new instance of the StoreDto class.
        /// </summary>
        public StoreDto()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the StoreDto class.
        /// </summary>
        public StoreDto(int? id = default(int?), string name = default(string), double? rating = default(double?), string officeHours = default(string))
        {
            Id = id;
            Name = name;
            Rating = rating;
            OfficeHours = officeHours;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "rating")]
        public double? Rating { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "officeHours")]
        public string OfficeHours { get; set; }

    }
}