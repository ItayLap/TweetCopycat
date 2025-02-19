namespace TweetCopycat.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }

        public string UserId { get; set; }

        public string Content {  get; set; }
        public bool IsRead {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
