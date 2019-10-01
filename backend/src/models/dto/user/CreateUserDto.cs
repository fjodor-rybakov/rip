namespace backend.models.dto.user
{
    public class CreateUserDto
    {
        public string Nickname { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}