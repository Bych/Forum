using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ResumeModel
    {
        [Required]
        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileId { get; set; }
    }
}