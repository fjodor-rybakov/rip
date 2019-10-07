using System.ComponentModel.DataAnnotations;

namespace backend.models.dto.news
{
    public class CreateNewsDto
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}