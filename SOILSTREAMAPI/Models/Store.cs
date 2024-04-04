using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOILSTREAMAPI.Models
{
    public class Store : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<StoreProduct> Products { get; set; }

    }
}
