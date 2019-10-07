using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.models.entities
{
    public class UserEntity : BaseEntity
    {
        public string Nickname { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public string Avatar { get; set; }

        public int RoleId { get; set; }
        
        public RoleEntity Role { get; set; }
        
        public List<NewsEntity> News { get; set; }
        
        public List<CommentEntity> CommentNews { get; set; }
    }
}