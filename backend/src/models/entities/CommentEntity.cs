namespace backend.models.entities
{
    public class CommentEntity : BaseEntity
    {
        public int UserId { get; set; }
        
        public int NewsId { get; set; }
        
        public string Value { get; set; }
        
        public UserEntity User { get; set; }

        public NewsEntity News { get; set; }
    }
}