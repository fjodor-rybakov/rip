using System.ComponentModel.DataAnnotations;

namespace backend.models.entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        
        public string Nickname { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}