using System.ComponentModel.DataAnnotations;

namespace VisualAcademy.Models
{
    public class Idea
    {
        public int Id { get; set; }
        [Required]
        public string Note { get; set; }
    }
}
