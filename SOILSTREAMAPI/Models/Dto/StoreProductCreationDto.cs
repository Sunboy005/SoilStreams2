using System.ComponentModel.DataAnnotations;

namespace SOILSTREAMAPI.Models.Dto
{
    public class StoreProductCreationDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public int AvailableQuantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than 0.")]
        public decimal UnitPrice { get; set; }
    }
}
