using System.Collections.Generic;

namespace backend.models.entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        
        public string RoleName { get; set; }
        
        public List<UserEntity> Users { get; set; }
    }
}