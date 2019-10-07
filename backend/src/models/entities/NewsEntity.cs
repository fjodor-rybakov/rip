using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.models.entities
{
    public class NewsEntity : BaseEntity
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<string> PathToImages { get; set; }

        public int UserId { get; set; }
        
        public UserEntity User { get; set; }
        
        public List<CommentEntity> CommentNews { get; set; }
    }
}