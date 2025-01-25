namespace TweetCopycat.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public String UserId { get; set; }

        public String Content {  get; set; }
        public bool IsRead {  get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
