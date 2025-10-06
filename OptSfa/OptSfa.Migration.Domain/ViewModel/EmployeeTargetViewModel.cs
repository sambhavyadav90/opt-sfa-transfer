namespace OptSfa.Migration.Domain.ViewModel
{
    public class EmployeeTargetViewModel
    {
        public string item_name { get; set; }
        public string item_newid { get; set; }
        public string item_pack_size { get; set; }
        public string item_code { get; set; }
        public float pts { get; set; }
        public float ptr { get; set; }
        public float mrp { get; set; }
        public float nrv { get; set; } = 0.0f;
        public float purchase_rate { get; set; }
    }
}