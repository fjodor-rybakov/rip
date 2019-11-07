using System;

namespace backend.models.dto.comment
{
    public class NewsCommentListDto
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public int NewsId { get; set; }
        
        public string Avatar { get; set; }
        
        public string Nickname { get; set; }

        public string Value { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}