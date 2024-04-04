namespace SOILSTREAMAPI.Models.Dto
{
    public class UserDetailsDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string Location { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public List<string> Role { get; set; } = new List<string>();
        public List<StoreProduct> StoreProducts { get; set; } 
        public List<Order> Orders { get; set; } 

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
