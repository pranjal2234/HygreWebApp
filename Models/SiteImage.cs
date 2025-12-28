using System;
using System.ComponentModel.DataAnnotations;

namespace HygreWebApp.Models
{
    public class SiteImage
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string Category { get; set; } // Homepage-Setup, Training, FAQ

        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
    }
}
