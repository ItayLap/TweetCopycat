namespace TweetCopycat.Models
{
    public class Like
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public string UserID { get; set; }
        public TweetModel Tweet { get; set; }
        public int TweetId {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
