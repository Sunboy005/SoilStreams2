namespace SOILSTREAMAPI.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
