namespace JongSnamFootball.Entities.Request
{
    public class SearchFieldRequest
    {
        public decimal? StartPrice { get; set; }

        public decimal? ToPrice { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }
    }
}
