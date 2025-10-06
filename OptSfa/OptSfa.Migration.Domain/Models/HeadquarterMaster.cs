namespace OptSfa.Migration.Domain.Models
{
    public class HeadquarterMaster
    {
        public int districtId { get; set; }
        public string district { get; set; }
        public int hqGroupId { get; set; }
        public string status { get; set; }
        public string districtCode { get; set; }
        public int stateMain { get; set; }
    }
}