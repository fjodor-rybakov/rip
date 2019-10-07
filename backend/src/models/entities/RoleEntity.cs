using System.Collections.Generic;

namespace backend.models.entities
{
    public class RoleEntity : BaseEntity
    {
        public string RoleName { get; set; }
        
        public List<UserEntity> Users { get; set; }
    }
}