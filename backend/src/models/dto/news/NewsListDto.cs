using System;
using System.Collections.Generic;

namespace backend.models.dto.news
{
    public class NewsListDto
    {
        public string Title { get; set; }
        
        public string Description{ get; set; }
        
        public List<string> PathToImages{ get; set; }
        
        public DateTime CreatedAt{ get; set; }
        
        public string Nickname{ get; set; }
        
        public string Avatar{ get; set; }
    }
}