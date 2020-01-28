using System.ComponentModel.DataAnnotations;

namespace ManufacturerAppCore.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ManufacturerCode { get; set; }
        public string Comment { get; set; }
    }
}
