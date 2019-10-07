namespace backend.models.dto.news
{
    public class UpdatedNewsDto
    {
        public string? Title { get; set; }
        
        public string? Description { get; set; }

        public int? UserId { get; set; }
    }
}