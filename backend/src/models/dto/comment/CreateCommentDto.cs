using System.ComponentModel.DataAnnotations;

namespace backend.models.dto.comment
{
    public class CreateCommentDto
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int NewsId { get; set; }
        
        [Required]
        public string Value { get; set; }
    }
}