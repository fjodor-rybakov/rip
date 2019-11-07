using System;
using System.Collections.Generic;
using backend.models.dto.comment;

namespace backend.models.dto.news
{
    public class NewsListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        
        public string Description{ get; set; }
        
        public List<string> PathToImages{ get; set; }
        
        public DateTime CreatedAt{ get; set; }
        
        public string Nickname{ get; set; }
        
        public string Avatar{ get; set; }
        
        public List<NewsCommentListDto>? Comments { get; set; }
    }
}