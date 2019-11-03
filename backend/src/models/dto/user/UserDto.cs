namespace backend.models.dto.user
{
    public class UserDto
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Info { get; set; }
        public int RoleId { get; set; }
    }
}