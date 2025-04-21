using System.ComponentModel.DataAnnotations;

namespace DocumentsWebApi.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
