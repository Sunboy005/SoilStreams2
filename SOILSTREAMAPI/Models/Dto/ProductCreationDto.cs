using System.ComponentModel.DataAnnotations;

namespace SOILSTREAMAPI.Models.Dto
{
    public class ProductCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public string Description { get; set; }
    }
}
