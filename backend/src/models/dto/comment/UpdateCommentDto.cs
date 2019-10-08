namespace backend.models.dto.comment
{
    public class UpdateCommentDto
    {
        #nullable enable
        public int? UserId { get; set; }
        
        public int? NewsId { get; set; }
        
        public string? Value { get; set; }
        #nullable disable
    }
}