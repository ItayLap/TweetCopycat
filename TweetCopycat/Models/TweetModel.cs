namespace TweetCopycat.Models
{
    public class TweetModel
    {
        public int Id { get; set; }
        public string UserID {  get; set; }
        public UserModel User { get; set; }
        public string Content {  get; set; }
        public bool IsRead {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
